using System;
using BetterTravel.Domain.Enums;

namespace BetterTravel.Domain.Cache
{
    public class CurrencyRate : Base.Cache
    {
        public CurrencyType Type { get; set; }
        public double Rate { get; set; }
        public DateTime ExchangeDate { get; set; }
    }
}