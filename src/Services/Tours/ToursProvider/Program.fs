module Program

open System
open System.Threading.Tasks
open System.Timers
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Application

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