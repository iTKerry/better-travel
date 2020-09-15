using System;
using CSharpFunctionalExtensions;

namespace BetterTravel.DataAccess.Abstractions.ValueObjects
{
    public class HotTourInfo : ValueObject<HotTourInfo>
    {
        protected HotTourInfo()
        {
        }
        
        public HotTourInfo(string name, Uri imageUri, Uri detailsUri)
        {
            Name = name;
            ImageUri = imageUri;
            DetailsUri = detailsUri;
        }

        public string Name { get; }
        public Uri ImageUri { get; }
        public Uri DetailsUri { get; }

        protected override bool EqualsCore(HotTourInfo other) =>
            Name == other.Name &&
            ImageUri == other.ImageUri &&
            DetailsUri == other.DetailsUri;

        protected override int GetHashCodeCore() => 
            Name.GetHashCode() + 
            ImageUri.GetHashCode() + 
            DetailsUri.GetHashCode();
    }
}