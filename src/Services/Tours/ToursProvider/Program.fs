module Program

open System
open System.Collections.Generic
open System.Net
open System.Linq
open System.Net.Http
open System.Threading.Tasks
open System.Timers
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Application
open RestSharp
open RestSharp

[<AbstractClass>]
type TimedHostedService(timer : Timer, logger : ILogger) =
    abstract member doWork : obj -> unit
    
    interface IHostedService with
        member this.StartAsync _ =
            logger.LogInformation "Timed Hosted Service running."
            timer.Elapsed.Add this.doWork
            timer.Start ()
            Task.CompletedTask
        
        member this.StopAsync _ =
            logger.LogInformation "Timed Hosted Service is stopping."
            timer.Stop ()
            Task.CompletedTask 
            
    interface IDisposable with
        member this.Dispose() =
            timer.Dispose()

type TestService(logger : ILogger<TestService>) =
    inherit TimedHostedService(new Timer(TimeSpan.FromSeconds(5.0).TotalMilliseconds), logger)
    override this.doWork _ =
        let providerUrl = "https://crm.*.ua/"
        let x = Uri($"{providerUrl}/LiveTourSearch/GetDirections")
        let x = Uri($"{providerUrl}/user/login")

        let content =
            [ Parameter("Login",      "", ParameterType.GetOrPost)
              Parameter("Password",   "",         ParameterType.GetOrPost)
              Parameter("RememberMe", "false",       ParameterType.GetOrPost) ]
            
        let client = RestClient(providerUrl, Timeout = -1)
        let request = RestRequest("/user/login", Method.POST, AlwaysMultipartFormData = true)
        request.Parameters.AddRange(content)
        let response = client.Execute(request)
        let cookies = response.Cookies |> Seq.toList
        
        let request = RestRequest("/api/Crm/ViewModeIsExistItemsGetInfo", Method.POST)
        cookies |> List.iter (fun cookie -> request.AddCookie(cookie.Name, cookie.Value) |> ignore)
        let request = request.AddParameter("currentUserId", "5072")
        
        let response = client.Execute(request)
        logger.LogInformation response.Content
                
        logger.LogInformation "Doing work."

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