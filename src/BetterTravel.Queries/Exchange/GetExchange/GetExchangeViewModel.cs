using System;

namespace BetterTravel.Queries.Exchange.GetExchange
{
    public class GetExchangeViewModel
    {
        public string Code { get; set; }
        public double Rate { get; set; }
        public DateTime Date { get; set; }
    }
}