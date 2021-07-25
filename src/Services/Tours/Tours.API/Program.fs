module Program

open FsToolkit.ErrorHandling
open HostedServices

open Giraffe

open FSharp.Control.Tasks

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting

[<CLIMutable>]
type HotelsRequest =
    { DirectionId : int
      ResortIds   : int list
      SearchTerm  : string option }

let directionsHandler : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        json Providers.directions next ctx

let resortsHandler (directionId : int) : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        match Providers.resorts directionId with
        | Some res -> json res next ctx
        | None     -> (RequestErrors.NOT_FOUND $"No resorts for direction %i{directionId}") next ctx


let private resortsResultAsync (ctx : HttpContext) = 
    ctx.TryBindQueryString<HotelsRequest>()
    |> Async.retn
    |> AsyncResult.bind ^fun request ->
       Providers.hotels request.DirectionId
                        request.ResortIds
                        request.SearchTerm

let hotelsHandler : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            match! resortsResultAsync ctx with
            | Ok hotels -> return! json hotels next ctx
            | Error err -> return! (RequestErrors.BAD_REQUEST err) next ctx
        }

let webApp =
    choose [
        route    "/ping"  >=> text "pong"
        subRoute "/api"
            (GET >=> choose [
                route  "/directions"        >=> directionsHandler
                routef "/directions/%i/resorts" resortsHandler
                route  "/hotels"            >=> hotelsHandler
            ])
            
        RequestErrors.NOT_FOUND "Not Found"
    ]

let configureApp (app : IApplicationBuilder) =
    app.UseGiraffe webApp

let configureServices (services : IServiceCollection) =
    services.AddGiraffe() |> ignore
    services.AddHostedService<ToursProviderService>() |> ignore

[<EntryPoint>]
let main _ =
    Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(
            fun webHostBuilder ->
                webHostBuilder
                    .Configure(configureApp)
                    .ConfigureServices(configureServices)
                    |> ignore)
        .Build()
        .Run()
    0