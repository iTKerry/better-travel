using System;
using System.Linq;
using BetterExtensions.Domain.Base;
using BetterTravel.DataAccess.Abstractions.Enums;

namespace BetterTravel.DataAccess.Abstractions.Entities.Enumerations
{
    public class Currency : Entity
    {
        public static readonly Currency UAH = Create(CurrencyType.UAH, "₴");
        public static readonly Currency USD = Create(CurrencyType.USD, "$");
        public static readonly Currency EUR = Create(CurrencyType.EUR, "€");
        
        public static readonly Currency[] AllCurrencies =
        {
            UAH, USD, EUR
        };
        
        protected Currency()
        {
        }

        private Currency(int id, string code, string sign)
            : base(id) =>
            (Code, Sign) = (code, sign);

        public string Code { get; }
        public string Sign { get; }

        public static Currency FromId(int id) =>
            AllCurrencies.SingleOrDefault(c => c.Id == id) 
            ?? throw new ArgumentException($"There is no such currency for id: {id}");

        public static Currency FromType(CurrencyType type) =>
            AllCurrencies.SingleOrDefault(c => c.Id == (int) type) 
            ?? throw new ArgumentException($"There is no such currency for type: {type}");
        
        public static Currency FromCode(string code) =>
            AllCurrencies.SingleOrDefault(c => c.Code == code) 
            ?? throw new ArgumentException($"There is no such currency for code: {code}");

        public bool IsType(CurrencyType type) =>
            Id == (int) type;
        
        private static Currency Create(CurrencyType type, string sign) =>
            new Currency((int) type, type.ToString(), sign);
    }
}