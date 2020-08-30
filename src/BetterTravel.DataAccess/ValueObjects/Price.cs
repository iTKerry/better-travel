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
        
        protected Price(int amount, PriceType type, Currency currency)
        {
            Amount = amount;
            Type = type;
            Currency = currency;
        }

        public int Amount { get; }
        public PriceType Type { get; }
        public virtual Currency Currency { get; }
        
        public static Price Create(int amount, PriceType type, Currency currency) =>
            new Price(amount, type, currency);
        
        public static Price FromUah(int amount, PriceType type) =>
            new Price(amount, type, UAH);
        
        protected override int GetHashCodeCore() => 
            Amount.GetHashCode() + Type.GetHashCode();

        protected override bool EqualCore(Price other) => 
            Amount == other.Amount && Type == other.Type;
    }
}