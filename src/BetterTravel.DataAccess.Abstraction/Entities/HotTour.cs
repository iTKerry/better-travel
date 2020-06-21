using System;
using BetterTravel.DataAccess.Abstraction.Entities.Base;
using BetterTravel.DataAccess.Abstraction.Entities.Enums;
using BetterTravel.DataAccess.Abstraction.ValueObjects;

namespace BetterTravel.DataAccess.Abstraction.Entities
{
    public class HotTour : Entity
    {
        public string Name { get; set; }
        public Stars Stars { get; set; }
        public Uri ImageUri { get; set; }
        public Uri DetailsUri { get; set; }
        public Duration Duration { get; set; }
        public Departure Departure { get; set; }
        public Price Price { get; set; }
        public Country Country { get; set; }
        public Resort Resort { get; set; }
    }
}