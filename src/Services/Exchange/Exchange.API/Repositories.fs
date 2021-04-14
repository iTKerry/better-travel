module Repositories

open System
open Microsoft.Extensions.Caching.Memory
open Monobank

module CurrencyRepo =
    
    type CurrencyData =
        { CodeA : int
          CodeB : int
          Date  : int
          Sell  : float
          Buy   : float
          Cross : float }
    
    [<Literal>]
    let private currencyKey = "_currency"
    let private options = MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(1.0))
        
    let update (cache : IMemoryCache) (data : CurrencyData []) =
        cache.Set(currencyKey, data, options)
    
    let getAsync (cache : IMemoryCache) =
        async {
            match cache.TryGetValue<CurrencyData []> currencyKey with
            | true, value -> return value
            | _ -> 
                let! data = Currency.AsyncLoad currencyUrl
                let result =
                    data
                    |> Array.map (fun curr ->
                        { CodeA = curr.CurrencyCodeA
                          CodeB = curr.CurrencyCodeB
                          Date  = curr.Date
                          Sell  = curr.RateSell  |> Option.map float |> Option.defaultValue 0.0 
                          Buy   = curr.RateBuy   |> Option.map float |> Option.defaultValue 0.0
                          Cross = curr.RateCross |> Option.map float |> Option.defaultValue 0.0 })
                return update cache result 
        }
