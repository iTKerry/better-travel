using BetterTravel.DataAccess.Entities;
using BetterTravel.DataAccess.Enums;
using BetterTravel.DataAccess.ValueObjects.Base;
using static BetterTravel.DataAccess.Entities.Currency;

namespace BetterTravel.DataAccess.ValueObjects
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