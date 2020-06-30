using System;
using BetterTravel.DataAccess.Entities.Enums;
using BetterTravel.DataAccess.ValueObjects.Base;

namespace BetterTravel.DataAccess.ValueObjects
{
    public class HotTourInfo : ValueObject<HotTourInfo>
    {
        protected HotTourInfo()
        {
        }
        
        public HotTourInfo(string name, DateTime departureDate, Uri imageUri, Uri detailsUri)
        {
            Name = name;
            DepartureDate = departureDate;
            ImageUri = imageUri;
            DetailsUri = detailsUri;
        }

        public string Name { get; }
        public DateTime DepartureDate { get; }
        public Uri ImageUri { get; }
        public Uri DetailsUri { get; }
        
        protected override int GetHashCodeCore() => 
            Name.GetHashCode() + 
            DepartureDate.GetHashCode() +
            ImageUri.GetHashCode() + 
            DetailsUri.GetHashCode();

        protected override bool EqualCore(HotTourInfo other) =>
            Name == other.Name &&
            DepartureDate == other.DepartureDate &&
            ImageUri == other.ImageUri &&
            DetailsUri == other.DetailsUri;
    }
}