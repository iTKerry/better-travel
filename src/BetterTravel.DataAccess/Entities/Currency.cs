using System;
using System.Linq;
using BetterTravel.DataAccess.Entities.Base;
using BetterTravel.DataAccess.Enums;

namespace BetterTravel.DataAccess.Entities
{
    public class Currency : Entity
    {
        public static readonly Currency UAH = new Currency((int) CurrencyType.UAH, CurrencyType.UAH.ToString());
        public static readonly Currency USD = new Currency((int) CurrencyType.USD, CurrencyType.USD.ToString());
        public static readonly Currency EUR = new Currency((int) CurrencyType.EUR, CurrencyType.EUR.ToString());
        
        public static readonly Currency[] AllCurrencies =
        {
            UAH, USD, EUR, 
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
            ?? throw new ArgumentException($"There is no such currency for id: {id}");

        public static Currency FromType(CurrencyType type) =>
            AllCurrencies.SingleOrDefault(c => c.Id == (int) type) 
            ?? throw new ArgumentException($"There is no such currency for type: {type}");
        
        public static Currency FromName(string name) =>
            AllCurrencies.SingleOrDefault(c => c.Name == name) 
            ?? throw new ArgumentException($"There is no such currency for type: {name}");

        public bool IsType(CurrencyType type) =>
            Id == (int) type;
    }
}