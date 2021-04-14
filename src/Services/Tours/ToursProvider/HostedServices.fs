module HostedServices

open System
open System.Net
open System.Threading.Tasks
open System.Timers
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging

open RestSharp

open Utils

[<AbstractClass>]
type TimedHostedService(timer : Timer, logger : ILogger) =
    abstract member doWork : obj -> unit
    
    interface IHostedService with
        member this.StartAsync _ =
            logger.LogInformation "Timed Hosted Service running."
            (*timer.Elapsed.Add this.doWork
            timer.Start ()*)
            this.doWork ()
            Task.CompletedTask
        
        member this.StopAsync _ =
            logger.LogInformation "Timed Hosted Service is stopping."
            timer.Stop ()
            Task.CompletedTask 
            
    interface IDisposable with
        member this.Dispose() =
            timer.Dispose()

type TestService(logger : ILogger<TestService>) =
    inherit TimedHostedService(new Timer(TimeSpan.FromSeconds(30.0).TotalMilliseconds), logger)
        
    let client (timeout : int) (providerUrl : string) =
        RestClient(providerUrl, Timeout = TimeSpan.FromSeconds(float timeout).Milliseconds)
        
    let defaultClient providerUrl =
        client 10 providerUrl

    let executeRequestAsync (client : RestClient, request : RestRequest) =
        async {
            match! client.ExecuteAsync(request) |> Async.AwaitTask with
            | response when response.StatusCode = HttpStatusCode.OK ->
                return response |> Ok 
            | response when response.StatusCode <> HttpStatusCode.OK ->
                return Error $"CODE: {response.StatusDescription}; MSG: {response.ErrorMessage}"
            | response ->
                return Error $"CODE: {response.StatusDescription}; MSG: {response.ErrorMessage}"
        }

    let loginCookiesStealer (url : string) (content : Map<string, _>) =
        async {
            let request = RestRequest(url, Method.POST, AlwaysMultipartFormData = true)
            content |> Map.iter (fun k v -> request.AddParameter(k, v) |> ignore)
            
            match! executeRequestAsync (defaultClient url, request) with
            | Ok response -> return response.Cookies |> Seq.toList |> Ok
            | Error error -> return Error error }

    let createRequest (method : Method) (cookies : RestResponseCookie list) =
        let request = RestRequest(method)
        cookies |> List.iter (fun cookie -> request.AddCookie(cookie.Name, cookie.Value) |> ignore)
        request
    
    let getToursList (request : RestRequest) =
        async {
            request.AddParameter("officeId", "381") |> ignore
            request.AddParameter("state", "8") |> ignore
            request.AddParameter("filterTourOfferId", "0") |> ignore
            
            match! executeRequestAsync (defaultClient Urls.getTourOfferListUri, request) with
            | Ok response -> return Ok response.Content
            | Error err   -> return Error err
        }
    
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
