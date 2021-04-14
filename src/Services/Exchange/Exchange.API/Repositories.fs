module Repositories

open System
open Microsoft.Extensions.Caching.Memory
open Monobank

module CurrencyRateRepo =
    
    type CurrencyRate =
        { CodeA : int
          CodeB : int
          Date  : int
          Sell  : float
          Buy   : float
          Cross : float }
    
    [<Literal>]
    let private currencyRateKey = "_currencyRate"
    let private options = MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(1.0))
        
    let update (cache : IMemoryCache) (rates : CurrencyRate []) =
        cache.Set(currencyRateKey, rates, options)
    
    let getAsync (cache : IMemoryCache) =
        async {
            match cache.TryGetValue<CurrencyRate []> currencyRateKey with
            | true, rates -> return rates
            | _ -> 
                let! jsonData = Currency.AsyncLoad currencyUrl
                let rates =
                    jsonData
                    |> Array.map (fun curr ->
                        { CodeA = curr.CurrencyCodeA
                          CodeB = curr.CurrencyCodeB
                          Date  = curr.Date
                          Sell  = curr.RateSell  |> Option.map float |> Option.defaultValue 0.0 
                          Buy   = curr.RateBuy   |> Option.map float |> Option.defaultValue 0.0
                          Cross = curr.RateCross |> Option.map float |> Option.defaultValue 0.0 })
                return update cache rates 
        }
