using System;
using BetterTravel.DataAccess.Abstraction.Entities.Enums;
using BetterTravel.DataAccess.Abstraction.ValueObjects;

namespace BetterTravel.DataAccess.Abstraction.Entities
{
    public class HotTour : Entity
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public DateTime DepartureDate { get; set; }
        public string DepartureLocation { get; set; }
        public DurationType DurationType { get; set; }
        public Stars Stars { get; set; }
        public PriceType PriceType { get; set; }

        public Country Country { get; set; }
        public Resort Resort { get; set; }
        
        public Uri ImageUri { get; set; }
        public Uri DetailsUri { get; set; }
    }
}