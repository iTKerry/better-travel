using System;
using BetterTravel.DataAccess.Abstraction.ValueObjects.Base;

namespace BetterTravel.DataAccess.Abstraction.ValueObjects
{
    public class Country : ValueObject<Country>
    {
        protected Country()
        {
        }

        public Country(string name, Uri detailsUri)
        {
            Name = name;
            DetailsUri = detailsUri;
        }
        
        public string Name { get; private set; }
        public Uri DetailsUri { get; private set; }
        
        protected override int GetHashCodeCore() => 
            Name.GetHashCode() + DetailsUri.GetHashCode();

        protected override bool EqualCore(Country other) => 
            Name == other.Name && DetailsUri == other.DetailsUri;
    }
}