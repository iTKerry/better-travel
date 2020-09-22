using System.Collections.Generic;
using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.Queries.HotTours.GetDepartures
{
    public class GetDeparturesQuery : IQuery<List<GetDeparturesViewModel>>
    {
        public bool Localize { get; set; }
    }
}