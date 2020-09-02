using System;

namespace BetterTravel.Queries.ViewModels
{
    public class GetExchangeViewModel
    {
        public string Code { get; set; }
        public double Rate { get; set; }
        public DateTime Date { get; set; }
    }
}