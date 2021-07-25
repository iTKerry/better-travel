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
        
    let requestSample (directionId : int) =
        [ "request[CurrentCountryId]", "37";
          "request[NetworkId]", "37";
          "request[CurrencyId]", "0";
          "request[DirectionId]", $"{directionId}";
          "request[ResortIds]", "";
          "request[HotelClassIds]", "";
          "request[HotelIds]", "";
          "request[DeparturePointIds]", "";
          "request[BoardIds]", "";
          "request[OperatorIds]", "";
          "request[DateFrom]", "27.07.21";
          "request[DateTo]", "03.08.21";
          "request[DurationFrom]", "6";
          "request[DurationTo]", "14";
          "request[Adults]", "2";
          "request[Child]", "0";
          "request[ChildAge1]", "-1";
          "request[ChildAge2]", "-1";
          "request[ChildAge3]", "-1";
          "request[EntityName]", "PSN.CRM";
          "request[Uid]", "5072";
          "request[IsExcursion]", "false";
          "request[IsHotTourMode]", "true";
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
          "request[ShowBlackList]", "true";
          "request[AviaOnly]", "false"; 
          "request[HotelCriteriaIds]", "";
          "keyword", "";
          "maximumRows", "0"; ]
        |> Map.ofList

    let getHotelsParam (directionId : int) (resortIds : int list) (term : string option) =
        let resIds = function
            | ids -> ids
                     |> List.map ^fun id -> id.ToString()
                     |> String.concat ","
        [ "maximumRows", "1000"
          "keyword", term |> Option.defaultValue ""
          "request[DirectionId]", directionId.ToString();
          "request[ResortIds]", resIds resortIds; ]
        |> Map.ofList

module Urls = 
    let baseUrl              = "https://crm.*.ua"
    let loginUri             = $"{baseUrl}/user/login"
    let getTourOfferListUri  = $"{baseUrl}/TourOffer/GetList"
    let getHotelsUri         = $"{baseUrl}/LiveTourSearch/GeHotels"
    let getDirectionsUri     = $"{baseUrl}/LiveTourSearch/GetDirections"
    let getResortListUri     = $"{baseUrl}/dictionary/directionResortGetList"
