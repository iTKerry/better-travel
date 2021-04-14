module HostedServices

open System
open System.Net
open System.Timers
open CustomHostedServices
open Microsoft.Extensions.Logging

open RestSharp

open Utils

let client (timeout : int) (providerUrl : string) =
    RestClient(providerUrl, Timeout = TimeSpan.FromSeconds(float timeout).Milliseconds)
    
let defaultClient providerUrl =
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

type ToursProviderService(logger : ILogger<ToursProviderService>) =
    inherit TimedHostedService(new Timer(TimeSpan.FromSeconds(30.0).TotalMilliseconds), logger)
        
    override this.doWork _ =
        async {
            logger.LogInformation "Doing work."
            
            match! loginCookiesStealer Urls.loginUri Configs.loginCredentials with
            | Ok cookies ->
                logger.LogInformation "Cookies stealing completed successfully."
                let baseRequest = createRequest Method.POST cookies
                
                match! getToursList baseRequest with
                | Ok tours  -> logger.LogInformation tours
                | Error err -> logger.LogError err
                
            | Error error -> logger.LogError error
            
        } |> Async.RunSynchronously
