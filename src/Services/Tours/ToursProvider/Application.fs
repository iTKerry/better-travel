[<AutoOpen>]
module Application

open System
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting

type AppState =
    { CliArguments    : string array option
      HostConfigs     : (IHostBuilder -> IHostBuilder) list
      ServicesConfigs : (IServiceCollection -> IServiceCollection) list }
    
type AppBuilder internal () =
    member _.Yield _ =
        { CliArguments = None
          HostConfigs = []
          ServicesConfigs = [] }
        
    member _.Run (state : AppState) : IHostBuilder =
        let host =
            Host.CreateDefaultBuilder(Option.toObj state.CliArguments)
            |> List.foldBack id state.HostConfigs
        
        host.ConfigureServices (fun svcs ->
            state.ServicesConfigs
            |> List.rev
            |> List.iter (fun fn -> fn svcs |> ignore))
        
    [<CustomOperation("host_config")>]
    member _.HostConfig (state, config) =
        { state with HostConfigs = config::state.HostConfigs }
        
    [<CustomOperation("service_config")>]
    member _.ServiceConfig(state, config) =
        { state with ServicesConfigs = config::state.ServicesConfigs }
    
    [<CustomOperation("cli_args")>]
    member _.CliArguments (state, args) =
        { state with CliArguments = Some args }
    
let application = AppBuilder()

let run (app: IHostBuilder) = app.Build().Run()
