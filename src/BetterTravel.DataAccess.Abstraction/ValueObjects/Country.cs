using System;

namespace BetterTravel.DataAccess.Abstraction.ValueObjects
{
    public class Country : ValueObject<Country>
    {
        protected Country()
        {
        }

        public Country(string name, Uri details)
            : base()
        {
            Name = name;
            Details = details;
        }
        
        public string Name { get; }
        public Uri Details { get; }
        
        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }

        protected override bool EqualCore(Country other)
        {
            throw new NotImplementedException();
        }
    }
}