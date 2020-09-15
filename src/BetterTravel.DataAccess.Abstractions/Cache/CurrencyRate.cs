using System;
using BetterTravel.DataAccess.Abstractions.Cache.Base;
using BetterTravel.DataAccess.Abstractions.Entities.Enumerations;
using BetterTravel.DataAccess.Abstractions.Enums;

namespace BetterTravel.DataAccess.Abstractions.Cache
{
    public class CurrencyRate : CachedObject
    {
        public CurrencyType Type { get; set; }
        public double Rate { get; set; }
        public DateTime ExchangeDate { get; set; }

        public override string ToString()
        {
            var currency = Currency.FromType(Type);
            return $"{currency.Code} ({currency.Sign}{Rate:C})";
        }
    }
}