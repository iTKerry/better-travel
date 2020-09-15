using System;
using System.Collections.Generic;
using BetterExtensions.Domain.Base;
using BetterTravel.DataAccess.Abstractions.Enums;

namespace BetterTravel.DataAccess.Abstractions.Views
{
    public class HotTourView : View
    {
        public string Name { get; set; }
        public string ResortName { get; set; }
        public int CountryId { get; set; }
        public int DepartureLocationId { get; set; }
        public HotelCategoryType HotelCategory { get; set; }
        public DateTime DepartureDate { get; set; }
        public Uri ImageLink { get; set; }
        public Uri DetailsLink { get; set; }
        public int DurationCount { get; set; }
        public DurationType DurationType { get; set; }
        public int PriceAmount { get; set; }
        public PriceType PriceType { get; set; }
        public int CurrencyId { get; set; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}