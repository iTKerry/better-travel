using BetterTravel.DataAccess.Abstraction.Entities.Enums;
using BetterTravel.DataAccess.Abstraction.ValueObjects.Base;

namespace BetterTravel.DataAccess.Abstraction.ValueObjects
{
    public class Price : ValueObject<Price>
    {
        protected Price()
        {
        }
        
        public Price(int count, PriceType type)
        {
            Count = count;
            Type = type;
        }

        public int Count { get; }
        public PriceType Type { get; }
        
        protected override int GetHashCodeCore() => 
            Count.GetHashCode() + Type.GetHashCode();

        protected override bool EqualCore(Price other) => 
            Count == other.Count && Type == other.Type;
    }
}