module Models


[<CLIMutable>]
type HotelsRequest =
    { DirectionId : int
      ResortIds   : int list
      SearchTerm  : string option }

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
