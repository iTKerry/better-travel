module Repositories

open System
open System.IO
open Microsoft.Extensions.Caching.Memory
open FSharp.Data
open Newtonsoft.Json

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
        let json = File.ReadAllText(currenciesPath)
        let data = JsonConvert.DeserializeObject<CurrencyInfo[]>(json)
        data

    let update (cache : IMemoryCache) (currencies : CurrencyInfo []) =
        cache.Set(currencyKey, currencies)
    
    let get (cache : IMemoryCache) =
        match cache.TryGetValue<CurrencyInfo []> currencyKey with
        | true, rates -> rates
        | _           -> update cache currencies 
