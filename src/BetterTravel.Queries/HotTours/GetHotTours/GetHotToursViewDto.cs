using System;
using BetterTravel.DataAccess.Enums;

namespace BetterTravel.Queries.HotTours.GetHotTours
{
    public class GetHotToursViewDto
    {
        public string Name { get; set; }
        public HotelCategoryType HotelCategory { get; set; }
        public Uri ImageLink { get; set; }
        public Uri DetailsLink { get; set; }
        public int DurationCount { get; set; }
        public DurationType DurationType { get; set; }
        public int DepartureLocationId { get; set; }
        public DateTime DepartureDate { get; set; }
        public double PriceAmount { get; set; }
        public PriceType PriceType { get; set; }
        public int PriceCurrencyId { get; set; }
        public int CountryId { get; set; }
        public string ResortName { get; set; }
    }
}