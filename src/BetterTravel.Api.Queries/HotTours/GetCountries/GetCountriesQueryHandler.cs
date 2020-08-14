using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Api.Queries.Abstractions;
using BetterTravel.Api.Queries.ViewModels;
using BetterTravel.Common.Localization;
using BetterTravel.Domain.Entities.Enumerations;
using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.Api.Queries.HotTours.GetCountries
{
    public class GetCountriesQueryHandler 
        : ApiQueryHandler<GetCountriesQuery, List<GetCountriesViewModel>>
    {
        public override Task<IHandlerResult<List<GetCountriesViewModel>>> Handle(
            GetCountriesQuery request, 
            CancellationToken cancellationToken)
        {
            var countries = Country.AllCountries
                .Select(country => new GetCountriesViewModel
                {
                    Id = country.Id, 
                    Name = request.Localize 
                        ? L.GetValue(country.Name, Culture.Ru)
                        : country.Name
                })
                .ToList();

            return Task.FromResult(Ok(countries));
        }
    }
}