using System.Collections.Generic;
using BetterTravel.DataAccess.Enums;
using BetterTravel.MediatR.Core.Abstractions;
using BetterTravel.Queries.ViewModels;

namespace BetterTravel.Queries.HotTours.GetHotTours
{
    public class GetHotToursQuery : IQuery<List<GetHotToursViewModel>>
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public int[] Countries { get; set; }
        public int[] Departures { get; set; }
        public int[] HotelCategories { get; set; }
        public bool Localize { get; set; }
        public CurrencyType Currency { get; set; }
    }
}