using System;
using BetterTravel.DataAccess.Entities.Enums;

namespace BetterTravel.DataAccess.Views
{
    public class HotTourView
    {
        public string Name { get; set; }
        public string ResortName { get; set; }
        public string CountryName { get; set; }
        public string DepartureName { get; set; }
        public string HotelCategory { get; set; }
        public DateTime DepartureDate { get; set; }
        public Uri ImageLink { get; set; }
        public Uri DetailsLink { get; set; }
        public int DurationCount { get; set; }
        public DurationType DurationType { get; set; }
        public int PriceAmount { get; set; }
        public PriceType PriceType { get; set; }
    }
}