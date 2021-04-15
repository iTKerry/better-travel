module Repositories

open System
open Microsoft.Extensions.Caching.Memory

module RateRepository =
    
    [<Literal>]
    let private rateKey = "_rate"
    let private options = MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(1.0))
        
    let update (cache : IMemoryCache) rates =
        cache.Set(rateKey, rates, options)
    
    let getAsync (cache : IMemoryCache) =
        async {
            match cache.TryGetValue rateKey with
            | true, rates -> return rates
            | _ -> 
                let! jsonData = Monobank.Rate.AsyncLoad Monobank.currencyUrl
                let rates =
                    jsonData
                    |> Array.map (fun curr ->
                        {| CodeA = curr.CurrencyCodeA
                           CodeB = curr.CurrencyCodeB
                           Date  = curr.Date
                           Sell  = curr.RateSell  |> Option.map float |> Option.defaultValue 0.0 
                           Buy   = curr.RateBuy   |> Option.map float |> Option.defaultValue 0.0
                           Cross = curr.RateCross |> Option.map float |> Option.defaultValue 0.0 |})
                return update cache (box rates) 
        }

module CurrencyRepository =
    
    open FSharp.Data
    
    [<Literal>]
    let private currencyKey = "_currency"
    
    [<Literal>]
    let private currenciesPath = "Seed/Currencies.json"
    type private Currencies = JsonProvider<currenciesPath>
    
    type CurrencyInfo =
        { Currency       : string
          Entity         : string
          AlphabeticCode : string
          NumericCode    : int }
    
    let private currencies =
        Currencies.Parse currenciesPath
        |> Array.map (fun curr ->
            { Currency       = curr.Currency
              Entity         = curr.Entity
              AlphabeticCode = curr.AlphabeticCode |> Option.defaultValue ""
              NumericCode    = curr.NumericCode    |> Option.defaultValue 0 })

    let update (cache : IMemoryCache) (currencies : CurrencyInfo []) =
        cache.Set(currencyKey, currencies)
    
    let get (cache : IMemoryCache) =
        match cache.TryGetValue<CurrencyInfo []> currencyKey with
        | true, rates -> rates
        | _           -> update cache currencies 
