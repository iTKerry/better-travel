module HostedServices

open System
open System.Net
open System.Timers
open CustomHostedServices
open Microsoft.Extensions.Logging

open RestSharp

open Utils

type ToursProviderService(logger : ILogger<ToursProviderService>) =
    inherit TimedHostedService(new Timer(TimeSpan.FromSeconds(30.0).TotalMilliseconds), logger)
        
    override this.doWork _ =
        async {
            logger.LogInformation "Doing work."
            
//            match! loginCookiesStealer Urls.loginUri Configs.loginCredentials with
//            | Ok cookies ->
//                logger.LogInformation "Cookies stealing completed successfully."
//                let baseRequest = createRequest Method.POST cookies
//                
//                match! getToursList baseRequest with
//                | Ok tours  -> logger.LogInformation tours
//                | Error err -> logger.LogError err
//                
//            | Error error -> logger.LogError error
            
        } |> Async.RunSynchronously
