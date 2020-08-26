using System.Collections.Generic;
using BetterTravel.MediatR.Core.Abstractions;
using BetterTravel.Queries.ViewModels;

namespace BetterTravel.Queries.HotTours.GetCountries
{
    public class GetCountriesQuery : IQuery<List<GetCountriesViewModel>>
    {
        public bool Localize { get; set; }
    }
}