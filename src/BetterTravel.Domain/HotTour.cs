using System;
using BetterTravel.Domain.Enums;
using CSharpFunctionalExtensions;

namespace BetterTravel.Domain
{
    public class HotTour
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public DateTime DepartureDate { get; set; }
        public string DepartureLocation { get; set; }
        public DurationType DurationType { get; set; }
        public Stars Stars { get; set; }
        public PriceType PriceType { get; set; }

        public Maybe<Country> Country { get; set; }
        public Maybe<Resort> Resort { get; set; }
        
        public Maybe<Uri> ImageUri { get; set; }
        public Maybe<Uri> DetailsUri { get; set; }
    }
}