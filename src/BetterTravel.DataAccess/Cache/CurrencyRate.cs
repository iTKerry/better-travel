using System;
using BetterTravel.DataAccess.Cache.Base;
using BetterTravel.DataAccess.Enums;

namespace BetterTravel.DataAccess.Cache
{
    public class CurrencyRate : CachedObject
    {
        public CurrencyType Type { get; set; }
        public double Rate { get; set; }
        public DateTime ExchangeDate { get; set; }

        public override string ToString() => 
            $"{Type}: {Rate:C}";
    }
}