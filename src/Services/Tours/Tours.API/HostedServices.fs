module HostedServices

open System
open System.Net
open System.Timers
open CustomHostedServices
open Microsoft.Extensions.Logging

open Newtonsoft.Json
open Newtonsoft.Json.Linq
open RestSharp

open Utils

let rec traverseResultM f list =

    let (>>=) x f = Result.bind f x
    let retn = Result.Ok

    let cons head tail = head :: tail

    match list with
    | [] ->
        retn []
    | head::tail ->
        f head                 >>= (fun h ->
        traverseResultM f tail >>= (fun t ->
        retn (cons h t) ))

let private client (timeout : int) (providerUrl : string) =
    RestClient(providerUrl, Timeout = TimeSpan.FromSeconds(float timeout).Milliseconds)
    
let private defaultClient providerUrl =
    client 10 providerUrl

let private executeRequestAsync (client : RestClient, request : RestRequest) =
    async {
        match! client.ExecuteAsync(request) |> Async.AwaitTask with
        | response when response.StatusCode = HttpStatusCode.OK ->
            return response |> Ok 
        | response when response.StatusCode <> HttpStatusCode.OK ->
            return Error $"CODE: {response.StatusDescription}; MSG: {response.ErrorMessage}"
        | response ->
            return Error $"CODE: {response.StatusDescription}; MSG: {response.ErrorMessage}"
    }

let private loginCookiesStealer (url : string) (content : Map<string, _>) =
    async {
        let request = RestRequest(url, Method.POST, AlwaysMultipartFormData = true)
        content |> Map.iter (fun k v -> request.AddParameter(k, v) |> ignore)
        
        match! executeRequestAsync (defaultClient url, request) with
        | Ok response -> return response.Cookies |> Seq.toList |> Ok
        | Error error -> return Error error }

let private createRequest (method : Method) (cookies : RestResponseCookie list) =
    let request = RestRequest(method)
    cookies |> List.iter (fun cookie -> request.AddCookie(cookie.Name, cookie.Value) |> ignore)
    request

let private getToursList (request : RestRequest) =
    async {
        Configs.activeTours |> Map.iter (fun k v -> request.AddParameter(k, v) |> ignore)
        
        match! executeRequestAsync (defaultClient Urls.getTourOfferListUri, request) with
        | Ok response -> return Ok response.Content
        | Error err   -> return Error err
    }

let private getDirections (request : RestRequest) =
    async {
        Configs.hotelsData 37
        |> List.iter (fun (l, r) -> request.AddParameter(l, r) |> ignore)
        
        match! executeRequestAsync (defaultClient Urls.getDirectionsUri, request) with
        | Ok response -> return Ok response.Content
        | Error err   -> return Error err
    }

let private getResorts (request : RestRequest) countryId =
    async {
        Configs.hotelsData countryId
        |> List.iter (fun (l, r) -> request.AddParameter(l, r) |> ignore)
        
        match! executeRequestAsync (defaultClient Urls.getResortListUri, request) with
        | Ok response -> return Ok (response.Content, countryId)
        | Error err   -> return Error err
    }

type ToursProviderService(logger : ILogger<ToursProviderService>) =
    inherit TimedHostedService(new Timer(TimeSpan.FromSeconds(30.0).TotalMilliseconds), logger)
        
    override this.doWork _ =
        async {
            logger.LogInformation "Doing work."
            
            match! loginCookiesStealer Urls.loginUri Configs.loginCredentials with
            | Ok cookies ->
                logger.LogInformation "Cookies stealing completed successfully."
                let baseRequest = createRequest Method.POST cookies
                
                match! getDirections baseRequest with
                | Ok directions  ->
                    let countries =
                        JObject.Parse(directions).["Items"].Value<JObject>().Properties()
                        |> Seq.map (fun x -> x.Value)
                        |> Seq.map (fun x -> {| Id = x.["Id"].Value<int>(); Name = x.["Name"].Value<string>() |})
                    
                    let! results = countries |> Seq.map (fun c -> getResorts baseRequest c.Id) |> Async.Sequential
                    match results |> Array.toList |> traverseResultM id with
                    | Ok directionsResorts ->
                        
                        let getResorts dr =
                            JObject.Parse(dr).["Items"].Value<JObject>().Properties()
                            |> Seq.map (fun x -> x.Value)
                            |> Seq.map (fun x -> {| Id = x.["Id"].Value<int>(); Name = x.["Name"].Value<string>() |})
                        
                        let result =
                            directionsResorts
                            |> List.map (fun (json, id) ->
                                ( getResorts json, (countries |> Seq.find (fun c -> c.Id = id))) )
                            |> List.map (fun (x, y) ->
                                {| Id      = y.Id
                                   Name    = y.Name
                                   Resorts = x |})
                        let json = JsonConvert.SerializeObject(result)
                        printfn $"%s{json}"
                        ()
                        
                    | Error err  -> logger.LogError err
                    
                    ()
                    
                | Error err -> logger.LogError err
                
                match! getToursList baseRequest with
                | Ok tours  -> logger.LogInformation tours
                | Error err -> logger.LogError err
                
            | Error error -> logger.LogError error
            
        } |> Async.RunSynchronously
