using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Abstraction.Entities;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using BetterTravel.Queries.Abstractions;
using BetterTravel.Queries.ViewModels;

namespace BetterTravel.Queries.Countries
{
    public class GetCountriesQueryHandler : QueryHandlerBase<GetCountriesQuery, List<GetCountriesViewModel>>
    {
        public override Task<IHandlerResult<List<GetCountriesViewModel>>> Handle(
            GetCountriesQuery request, 
            CancellationToken cancellationToken)
        {
            var countries = Country.AllCountries
                .Select(c => new GetCountriesViewModel {Id = c.Id, Name = c.Name})
                .ToList();

            return Task.FromResult(Ok(countries));
        }
    }
}