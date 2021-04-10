module Program

module Configs =
    let loginCredentials =
        [ "Login",      "тестировщик";
          "Password",   "111";
          "RememberMe", "false" ]
        |> Map.ofList

module Urls = 
    let baseUrl = "https://crm.*.ua"
    let loginUri = $"{baseUrl}/user/login"
    let getListUri = $"{baseUrl}/TourOffer/GetList"


open System
open System.Net
open System.Timers
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open RestSharp

open Application
open HostedServices


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

type TestService(logger : ILogger<TestService>) =
    inherit TimedHostedService(new Timer(TimeSpan.FromSeconds(10.0).TotalMilliseconds), logger)
    override this.doWork _ =
        async {
            logger.LogInformation "Doing work."
            
            match! loginCookiesStealer Urls.loginUri Configs.loginCredentials with
            | Ok cookies ->
                logger.LogInformation "Steal cookies successfully."
                
                let request = RestRequest(Method.POST)
                cookies |> List.iter (fun cookie -> request.AddCookie(cookie.Name, cookie.Value) |> ignore)
                
                request.AddParameter("officeId", " 381") |> ignore
                request.AddParameter("state", " 2") |> ignore
                request.AddParameter("filterTourOfferId", " 0") |> ignore
                
                match! executeRequestAsync (defaultClient Urls.getListUri, request) with
                | Ok response ->
                    logger.LogInformation response.Content
                | Error error ->
                    logger.LogError error
                    
            | Error error -> logger.LogError error
            
        } |> Async.RunSynchronously

let configureLogging (builder : ILoggingBuilder) = 
    builder.AddConsole().AddDebug() |> ignore
    
let app argv =
    application {
        cli_args argv
        host_config (fun cfg -> cfg.ConfigureLogging configureLogging)
        service_config (fun cfg -> cfg.AddHostedService<TestService>())
    }
    
[<EntryPoint>]
let main argv =
    app argv |> run
    0