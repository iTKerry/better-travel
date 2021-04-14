module HostedServices

open System
open System.Timers
open Microsoft.Extensions.Caching.Memory
open Microsoft.Extensions.Logging

open CustomHostedServices
open Repositories

type CurrencyProviderService(logger : ILogger<CurrencyProviderService>, cache : IMemoryCache) =
    inherit TimedHostedService(new Timer(TimeSpan.FromHours(1.0).TotalMilliseconds), logger)
    override this.doWork _ =
        async {
            do! CurrencyRateRepo.getAsync cache |> Async.Ignore
        } |> Async.RunSynchronously
