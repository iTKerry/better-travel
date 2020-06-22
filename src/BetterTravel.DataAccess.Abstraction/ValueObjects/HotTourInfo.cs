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
        
        public HotTourInfo(string name, Stars stars, Uri imageUri, Uri detailsUri)
        {
            DetailsUri = detailsUri;
            ImageUri = imageUri;
            Stars = stars;
            Name = name;
        }

        public string Name { get; }
        public Stars Stars { get; }
        public Uri ImageUri { get; }
        public Uri DetailsUri { get; }
        
        protected override int GetHashCodeCore() => 
            Name.GetHashCode() + 
            Stars.GetHashCode() + 
            ImageUri.GetHashCode() + 
            DetailsUri.GetHashCode();

        protected override bool EqualCore(HotTourInfo other) =>
            Name == other.Name &&
            Stars == other.Stars &&
            ImageUri == other.ImageUri &&
            DetailsUri == other.DetailsUri;
    }
}