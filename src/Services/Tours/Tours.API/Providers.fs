module Providers

open System.IO
open Newtonsoft.Json
open Newtonsoft.Json.Linq
open RestSharp
open Utils
open FsToolkit.ErrorHandling

type Resort =
    { Id   : int
      Name : string }

type Direction =
    { Id      : int
      Name    : string
      Resorts : Resort [] }

type Operator = {
    OperatorId               : int 
    OperatorName             : string 
    SearchStatusDescriptions : string 
    SearchIsFinished         : bool 
    MinPrice                 : int option 
    CurrencySign             : string 
}

type Price = {
    TourName            : string 
    LevelNo             : int 
    PriceId             : int 
    LocaleCountryPrice  : int 
    LocaleCountrySign   : string 
    CurrencyPrice       : int 
    CurrencySign        : string 
    HotelMinPrice       : int option
    HotelId             : int 
    HotelClassName      : string 
    CompanyHotelUrl     : string 
    HotelDetailsURLPath : string 
    Duration            : int 
    DurationStr         : string 
    AgentBonus          : string
    DepartureDate       : string 
    HotelName           : string 
    IsBestSeller        : bool 
    IsRecommended       : bool 
    ResortName          : string 
    BoardName           : string
    BoardSign           : string 
    IsPromo             : bool 
    DeparturePointName  : string 
    ApartmentName       : string 
    OperatorName        : string 
    HashCode            : string 
    IsComment           : bool option
    IsGds               : bool 
    IsFavorit           : bool 
    RatingsValue        : double option
    HasTourSearchKey    : bool option
    IsFavoriteByUser    : bool 
    IsBlackListByUser   : bool 
    IsBlackList         : bool 
}

type RootItem = {
    TourName            : string 
    LevelNo             : int 
    PriceId             : int 
    LocaleCountryPrice  : int 
    LocaleCountrySign   : string 
    CurrencyPrice       : int 
    CurrencySign        : string 
    HotelMinPrice       : int option
    HotelId             : int 
    HotelClassName      : string 
    CompanyHotelUrl     : string 
    HotelDetailsURLPath : string 
    Duration            : int 
    DurationStr         : string 
    AgentBonus          : string 
    DepartureDate       : string 
    HotelName           : string 
    IsBestSeller        : bool 
    IsRecommended       : bool 
    ResortName          : string 
    BoardName           : string 
    BoardSign           : string 
    IsPromo             : bool 
    DeparturePointName  : string 
    ApartmentName       : string 
    OperatorName        : string 
    HashCode            : string 
    IsComment           : bool option 
    IsGds               : bool 
    IsFavorit           : bool 
    RatingsValue        : double option
    HasTourSearchKey    : bool option
    IsFavoriteByUser    : bool 
    IsBlackListByUser   : bool 
    IsBlackList         : bool 
}

type Statistic = {
    TotalPriceQty     : int 
    SearchIsFinished  : bool 
    PriceLimitsStr    : string 
    GetNextCmdAllowed : bool 
}

type ListItem = {
    RootItem : RootItem
    Prices : List<Price>
}

type SearchRoot = {
    RequestId         : int 
    Statistic         : Statistic 
    Operators         : Operator list 
    List              : ListItem list 
    CurrencyId        : int 
    PriceFrom         : int 
    PriceTo           : int 
    GroupByHotel      : bool 
    ShowPromoPrice    : bool 
    ShowGdsPrice      : bool 
    AviaOnly          : bool 
    ShowBlackList     : bool 
    HotelsQty         : int 
    MaximumRows       : int 
    PricesForShowQty  : int 
}

let private directionsWithResorts =
    let json = File.ReadAllText("directions_with_resorts.json")
    JsonConvert.DeserializeObject<Direction array>(json)

let directions =
    directionsWithResorts
    |> Array.map (fun x -> {| Id = x.Id; Name = x.Name |})
    
let resorts directionId =
    directionsWithResorts
    |> Array.tryFind (fun x -> x.Id = directionId)
    |> Option.map    (fun x -> x.Resorts)
    
let private enrichWithParams dirId resIds term (req : RestRequest) =
    Configs.getHotelsParam dirId resIds term
    |> Map.iter (fun k v -> req.AddParameter(k, v) |> ignore)
    req

let private getHotels (req : RestRequest) =
    async {
        let parseResponse (response : IRestResponse) =
            JObject.Parse(response.Content).["Items"].Value<JObject>().Properties()
            |> Seq.map (fun x -> x.Value)
            |> Seq.map (fun x -> { Id = x.["Id"].Value<int>(); Name = x.["Name"].Value<string>() })
            |> Seq.toList
        
        let! response = AuthorizedHttpClient.executeRequestAsync (Urls.getHotelsUri, req)
        return response |> Result.map parseResponse
    }
    
let hotels directionId resorts term =
    asyncResult {
        let! request = AuthorizedHttpClient.createRequestAsync Method.POST
        let request = enrichWithParams directionId resorts term request
        return! getHotels request
    }