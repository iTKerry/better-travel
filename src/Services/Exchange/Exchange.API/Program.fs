﻿module Program

open HostedServices
open Giraffe
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Caching.Memory
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Hosting

open FSharp.Control.Tasks
open Repositories

let rateHandler : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let cache = ctx.GetService<IMemoryCache>()
            let! data = CurrencyRateRepo.getAsync cache
            return! json data next ctx
        }

let webApp =
    choose [
        subRoute "/api"
            (GET >=> choose [
                route "/rate" >=> rateHandler
            ])
    ]

let configureApp (app : IApplicationBuilder) =
    app.UseGiraffe webApp

let configureServices (services : IServiceCollection) =
    services.AddGiraffe() |> ignore
    services.AddMemoryCache() |> ignore
    services.AddHostedService<CurrencyProviderService>() |> ignore

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