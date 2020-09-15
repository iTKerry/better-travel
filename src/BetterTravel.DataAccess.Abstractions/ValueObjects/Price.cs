using BetterTravel.DataAccess.Abstractions.Entities.Enumerations;
using BetterTravel.DataAccess.Abstractions.Enums;
using CSharpFunctionalExtensions;
using static BetterTravel.DataAccess.Abstractions.Entities.Enumerations.Currency;

namespace BetterTravel.DataAccess.Abstractions.ValueObjects
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

        protected override bool EqualsCore(Price other) => 
            Amount == other.Amount && Type == other.Type;

        protected override int GetHashCodeCore() => 
            Amount.GetHashCode() + Type.GetHashCode();
    }
}