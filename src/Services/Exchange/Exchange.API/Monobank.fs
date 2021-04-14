module Monobank

open FSharp.Data

[<Literal>]
let currencyUrl = "https://api.monobank.ua/bank/currency"

type Currency = JsonProvider<currencyUrl>

