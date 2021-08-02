module Program

open Errors
open FsToolkit.ErrorHandling
open FsToolkit.ErrorHandling.Operator.AsyncResult

open Models
open HostedServices

open Giraffe

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting

let mapError = function
    | Domain err ->
        match err with
        | DirectionNotFound directionId ->
            RequestErrors.NOT_FOUND $"No resorts for direction %i{directionId}"
            
    | Parser err ->
        match err with
        | InvalidJsonResponse res ->
            ServerErrors.INTERNAL_ERROR $"Cannot process json. Details: {res}"
        | InvalidJsonRequest  req ->
            RequestErrors.BAD_REQUEST $"Invalid request: %s{req}"
        
    | Client err ->
        match err with
        | RequestError (url, code, msg) ->
            ServerErrors.INTERNAL_ERROR $"Error with Client for url %s{url} with code {code} and message: {msg}"

let resultAsJson next ctx = function
    | Ok data -> json data next ctx 
    | Error err -> mapError err next ctx


let directionsHandler : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        json Providers.directions next ctx

let resortsHandler (directionId : int) : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        resultAsJson next ctx (Providers.resorts directionId)

let private resortsResultAsync (ctx : HttpContext) =
    ctx.TryBindQueryString<HotelsRequest>()
    |>  Result.mapError ^fun err -> InvalidJsonRequest err |> AppError.create 
    |>  Async.retn
    >>= Providers.hotels

let hotelsHandler : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        resortsResultAsync ctx
        |> Async.map (resultAsJson next ctx)
        |> Async.RunSynchronously

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