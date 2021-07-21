module Program

open System.IO
open System.Net.Http.Json
open HostedServices

open Giraffe

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Newtonsoft.Json

type Resort =
    { Id   : int
      Name : string }

type Direction =
    { Id      : int
      Name    : string
      Resorts : Resort [] }

let directionsWithResorts =
    let json = File.ReadAllText("directions_with_resorts.json")
    let data = JsonConvert.DeserializeObject<Direction array>(json)
    data

let directionsHandler : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        let directions =
            directionsWithResorts
            |> Array.map (fun x -> {| Id = x.Id; Name = x.Name |})
        json directions next ctx

let resortsHandler (directionId : int) : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        let resorts =
            directionsWithResorts
            |> Array.tryFind (fun x -> x.Id = directionId)
            |> Option.map (fun x -> x.Resorts)
            
        match resorts with
        | Some res -> json res next ctx
        | None     -> (RequestErrors.NOT_FOUND $"No resorts for direction %i{directionId}") next ctx

let webApp =
    choose [
        route "/ping"   >=> text "pong"
        
        subRoute "/api"
            (GET >=> choose [
                route "/directions" >=> directionsHandler
                routef "/directions/%i/resorts" resortsHandler
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