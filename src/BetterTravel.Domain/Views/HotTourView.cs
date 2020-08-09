using System;
using BetterTravel.Domain.Entities;
using BetterTravel.Domain.Entities.Enumerations;
using BetterTravel.Domain.Enums;
using BetterTravel.Domain.Views.Base;

namespace BetterTravel.Domain.Views
{
    public class HotTourView : View
    {
        public string Name { get; set; }
        public string ResortName { get; set; }
        public Country Country { get; set; }
        public DepartureLocation DepartureLocation { get; set; }
        public HotelCategory HotelCategory { get; set; }
        public DateTime DepartureDate { get; set; }
        public Uri ImageLink { get; set; }
        public Uri DetailsLink { get; set; }
        public int DurationCount { get; set; }
        public DurationType DurationType { get; set; }
        public int PriceAmount { get; set; }
        public PriceType PriceType { get; set; }
    }
}