using System;
using BetterTravel.DataAccess.Abstraction.ValueObjects.Base;

namespace BetterTravel.DataAccess.Abstraction.ValueObjects
{
    public class Resort : ValueObject<Resort>
    {
        protected Resort()
        {
        }
        
        public Resort(string name, Uri details)
            : base()
        {
            Name = name;
            Details = details;
        }
        
        public string Name { get; }
        public Uri Details { get; }
        
        protected override int GetHashCodeCore() => 
            Name.GetHashCode() + Details.GetHashCode();

        protected override bool EqualCore(Resort other) => 
            Name == other.Name && Details == other.Details;
    }
}