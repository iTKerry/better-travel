module Utils


module Configs =
    let loginCredentials =
        [ "Login",      "тестировщик";
          "Password",   "111";
          "RememberMe", "false" ]
        |> Map.ofList
    
    let activeTours =
        [ "officeId",          381
          "state",             8
          "filterTourOfferId", 0 ]
        |> Map.ofList
        
    let hotelsData (countryId : int) =
        [ "request[CurrentCountryId]", $"{countryId}";
          "request[NetworkId]", "37";
          "request[CurrencyId]", "0";
          "request[DirectionId]", "360";
          "request[ResortIds]", "";
          "request[HotelClassIds]", "";
          "request[HotelIds]", "";
          "request[DeparturePointIds]", "1";
          "request[BoardIds]", "";
          "request[OperatorIds]", "";
          "request[DateFrom]", "15.04.2021";
          "request[DateTo]", "24.04.2021";
          "request[DurationFrom]", "6";
          "request[DurationTo]", "8";
          "request[Adults]", "2";
          "request[Child]", "0";
          "request[ChildAge1]", "0";
          "request[ChildAge2]", "0";
          "request[ChildAge3]", "0";
          "request[EntityName]", "PSN.CRM";
          "request[Uid]", "5072";
          "request[IsExcursion]", "false";
          "request[IsHotTourMode]", " true";
          "request[PriceFrom]", "0";
          "request[PriceTo]", "0";
          "request[UserCurrency][CurrencyId]", "0";
          "request[UserCurrency][CurrencyName]", "Гривна";
          "request[UserCurrency][ShortName]", "UAH";
          "request[UserCurrency][Template]", "{0} грн.";
          "request[UserCurrency][Sign]", "грн";
          "request[UserCurrency][IsDefault]", "true";
          "request[UserCurrency][Alias]", "грн.";
          "request[LeadId]", "0";
          "request[ContactId]", "0";
          "request[OpportunityId]", "0";
          "request[TourIsSelected]", "false";
          "request[GroupByHotel]", "true";
          "request[AllowPromoPrice]", "true";
          "request[ShowPromoPrice]", "false";
          "request[ShowGdsPrice]", "false";
          "request[ShowBlackList]", "false";
          "request[AviaOnly]", "false";
          "request[HotelCriteriaIds]", "";
          "keyword", "";
          "maximumRows", "0"; ]

module Urls = 
    let baseUrl = "https://crm.*.ua"
    let loginUri = $"{baseUrl}/user/login"
    let getTourOfferListUri = $"{baseUrl}/TourOffer/GetList"
    let getHotelsUri = $"{baseUrl}/LiveTourSearch/GeHotels"
    let getResortListUri = $"{baseUrl}/dictionary/directionResortGetList"
