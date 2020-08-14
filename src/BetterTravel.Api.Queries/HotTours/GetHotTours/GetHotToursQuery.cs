using System.Collections.Generic;
using BetterTravel.Api.Queries.ViewModels;
using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.Api.Queries.HotTours.GetHotTours
{
    public class GetHotToursQuery : IQuery<List<GetHotToursViewModel>>
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public int[] Countries { get; set; }
        public int[] Departures { get; set; }
        public int[] HotelCategories { get; set; }
        public bool Localize { get; set; }
    }
}