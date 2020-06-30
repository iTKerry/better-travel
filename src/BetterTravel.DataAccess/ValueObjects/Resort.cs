using System;
using BetterTravel.DataAccess.ValueObjects.Base;

namespace BetterTravel.DataAccess.ValueObjects
{
    public class Resort : ValueObject<Resort>
    {
        protected Resort()
        {
        }
        
        public Resort(string name)
        {
            Name = name;
        }
        
        public string Name { get; }
        
        protected override int GetHashCodeCore() => 
            Name.GetHashCode();

        protected override bool EqualCore(Resort other) => 
            Name == other.Name;
    }
}