using System;
using BetterTravel.DataAccess.Abstraction.ValueObjects.Base;

namespace BetterTravel.DataAccess.Abstraction.ValueObjects
{
    public class Country : ValueObject<Country>
    {
        protected Country()
        {
        }

        public Country(string name, Uri details)
        {
            Name = name;
            Details = details;
        }
        
        public string Name { get; }
        public Uri Details { get; }
        
        protected override int GetHashCodeCore() => 
            Name.GetHashCode() + Details.GetHashCode();

        protected override bool EqualCore(Country other) => 
            Name == other.Name && Details == other.Details;
    }
}