using System;
using BetterTravel.Domain.Enums;

namespace BetterTravel.Queries.ViewModels
{
    public class GetHotToursViewModel
    {
        public string Name { get; set; }
        public string HotelCategory { get; set; }
        public Uri ImageLink { get; set; }
        public Uri DetailsLink { get; set; }
        public int DurationCount { get; set; }
        public DurationType DurationType { get; set; }
        public string DepartureLocationName { get; set; }
        public DateTime DepartureDate { get; set; }
        public int PriceAmount { get; set; }
        public PriceType PriceType { get; set; }
        public string CountryName { get; set; }
        public string ResortName { get; set; }
    }
}