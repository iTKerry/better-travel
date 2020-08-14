using System;
using System.Collections.Generic;

namespace BetterTravel.HotToursFetcher.Function.Responses
{
    public class TourListItemResponse
    {
        public string TourName { get; set; }

        public List<int> StarsCount { get; set; }

        public string Date { get; set; }
        public string DepartureDate { get; set; }
        public string DeparturePointNameGenitive { get; set; }
        
        public int DurationDay { get; set; }
        public string NightsText { get; set; }

        public int PriceFrom { get; set; }
        public string PriceFromString { get; set; }
        public string PriceDescription { get; set; }
        
        public Uri ImageUrl { get; set; }
        public Uri TourDetailsUrl { get; set; }
        public Uri TourOrderUrl { get; set; }
        
        public LinksResponse CountryLinks { get; set; }
        public LinksResponse ResortLinks { get; set; }
        public string ButtonText { get; set; }
    }
}