using BetterTravel.Domain.Enums;
using BetterTravel.Domain.ValueObjects.Base;

namespace BetterTravel.Domain.ValueObjects
{
    public class Price : ValueObject<Price>
    {
        protected Price()
        {
        }
        
        public Price(int amount, PriceType type)
        {
            Amount = amount;
            Type = type;
        }

        public int Amount { get; }
        public PriceType Type { get; }
        
        protected override int GetHashCodeCore() => 
            Amount.GetHashCode() + Type.GetHashCode();

        protected override bool EqualCore(Price other) => 
            Amount == other.Amount && Type == other.Type;
    }
}