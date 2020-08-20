using BetterTravel.Domain.Entities;
using BetterTravel.Domain.Enums;
using BetterTravel.Domain.ValueObjects.Base;
using static BetterTravel.Domain.Entities.Currency;

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
            Currency = UAH;
        }

        public int Amount { get; }
        public PriceType Type { get; }
        public virtual Currency Currency { get; }
        
        protected override int GetHashCodeCore() => 
            Amount.GetHashCode() + Type.GetHashCode();

        protected override bool EqualCore(Price other) => 
            Amount == other.Amount && Type == other.Type;
    }
}