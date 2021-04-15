module HostedServices

open System
open System.Threading.Tasks
open System.Timers

open Microsoft.Extensions.Caching.Memory
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging

open CustomHostedServices
open Repositories

type RateProviderService(logger : ILogger<RateProviderService>, cache : IMemoryCache) =
    inherit TimedHostedService(new Timer(TimeSpan.FromHours(1.0).TotalMilliseconds), logger)
        override this.doWork _ =
            async {
                do! RateRepository.getAsync cache |> Async.Ignore
            } |> Async.RunSynchronously
        
type CurrencyProviderService(logger : ILogger<CurrencyProviderService>, cache : IMemoryCache) =
    interface IHostedService with
        member this.StartAsync _ =
            logger.LogInformation "Start currency provider"
            CurrencyRepository.get cache |> ignore
            Task.CompletedTask

        member this.StopAsync _ =
            logger.LogInformation "Stop currency provider"
            Task.CompletedTask
