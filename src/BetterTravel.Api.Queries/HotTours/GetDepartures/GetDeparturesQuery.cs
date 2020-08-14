using System.Collections.Generic;
using BetterTravel.Api.Queries.ViewModels;
using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.Api.Queries.HotTours.GetDepartures
{
    public class GetDeparturesQuery : IQuery<List<GetDeparturesViewModel>>
    {
        public bool Localize { get; set; }
    }
}