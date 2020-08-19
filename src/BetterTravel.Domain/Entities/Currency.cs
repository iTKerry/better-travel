using System.Linq;
using BetterTravel.Domain.Entities.Base;
using BetterTravel.Domain.Enums;

namespace BetterTravel.Domain.Entities
{
    public class Currency : Entity
    {
        public static Currency Unknown = new Currency((int) CurrencyType.Other, CurrencyType.Other.ToString());
        public static Currency UAH = new Currency((int) CurrencyType.UAH, CurrencyType.UAH.ToString());
        public static Currency USD = new Currency((int) CurrencyType.USD, CurrencyType.USD.ToString());
        public static Currency EUR = new Currency((int) CurrencyType.EUR, CurrencyType.EUR.ToString());
        
        public static Currency[] AllCurrencies =
        {
            Unknown, UAH, USD, EUR, 
        };
        
        protected Currency()
        {
        }

        private Currency(int id, string name) 
            : base(id) => 
            Name = name;
        
        public string Name { get; }

        public static Currency FromId(int id) =>
            AllCurrencies.SingleOrDefault(c => c.Id == id) 
            ?? Unknown;

        public static Currency FromType(CurrencyType type) =>
            AllCurrencies.SingleOrDefault(c => c.Id == (int) type) 
            ?? Unknown;
    }
}