module Program

open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection

open Application
open HostedServices

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