using System.Collections.Generic;
using BetterTravel.Api.Queries.ViewModels;
using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.Api.Queries.HotTours.GetCountries
{
    public class GetCountriesQuery : IQuery<List<GetCountriesViewModel>>
    {
        public bool Localize { get; set; }
    }
}