using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Abstraction.Entities;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using BetterTravel.Queries.Abstractions;
using BetterTravel.Queries.ViewModels;

namespace BetterTravel.Queries.Departures
{
    public class GetDeparturesQueryHandler 
        : QueryHandlerBase<GetDeparturesQuery, List<GetDeparturesViewModel>>
    {
        public override Task<IHandlerResult<List<GetDeparturesViewModel>>> Handle(
            GetDeparturesQuery request, 
            CancellationToken cancellationToken)
        {
            var departures = DepartureLocation.AllDepartures
                .Select(d => new GetDeparturesViewModel {Id = d.Id, Name = d.Name})
                .ToList();
            
            return Task.FromResult(Ok(departures));
        }
    }
}