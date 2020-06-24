using System;
using BetterTravel.DataAccess.Abstraction.Entities.Enums;
using BetterTravel.DataAccess.Abstraction.ValueObjects.Base;

namespace BetterTravel.DataAccess.Abstraction.ValueObjects
{
    public class HotTourInfo : ValueObject<HotTourInfo>
    {
        protected HotTourInfo()
        {
        }
        
        public HotTourInfo(string name, Stars stars, DateTime departureDate, Uri imageUri, Uri detailsUri)
        {
            Name = name;
            Stars = stars;
            DepartureDate = departureDate;
            ImageUri = imageUri;
            DetailsUri = detailsUri;
        }

        public string Name { get; }
        public Stars Stars { get; }
        public DateTime DepartureDate { get; }
        public Uri ImageUri { get; }
        public Uri DetailsUri { get; }
        
        protected override int GetHashCodeCore() => 
            Name.GetHashCode() + 
            Stars.GetHashCode() +
            DepartureDate.GetHashCode() +
            ImageUri.GetHashCode() + 
            DetailsUri.GetHashCode();

        protected override bool EqualCore(HotTourInfo other) =>
            Name == other.Name &&
            Stars == other.Stars &&
            DepartureDate == other.DepartureDate &&
            ImageUri == other.ImageUri &&
            DetailsUri == other.DetailsUri;
    }
}