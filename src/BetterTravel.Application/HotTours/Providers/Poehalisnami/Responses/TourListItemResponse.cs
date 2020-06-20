using System;
using System.Collections.Generic;

namespace BetterTravel.Application.HotTours.Providers.Poehalisnami.Responses
{
    public class TourListItemResponse
    {
        public List<long> StarsCount { get; set; }
        public string DepartureDate { get; set; }
        public long DurationDay { get; set; }
        public long PriceFrom { get; set; }
        public string PriceFromString { get; set; }
        public string PriceDescription { get; set; }
        public string TourName { get; set; }
        public string DeparturePointNameGenitive { get; set; }
        public string Date { get; set; }
        public string NightsText { get; set; }
        public Uri ImageUrl { get; set; }
        public Uri TourDetailsUrl { get; set; }
        public Uri TourOrderUrl { get; set; }
        public LinksResponse CountryLinks { get; set; }
        public LinksResponse ResortLinks { get; set; }
        public string ButtonText { get; set; }
    }
}