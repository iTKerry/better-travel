using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Abstraction.Repositories;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using BetterTravel.Queries.Abstractions;
using BetterTravel.Queries.ViewModels;

namespace BetterTravel.Queries.HotTours
{
    public class GetHotToursQueryHandler : QueryHandlerBase<GetHotToursQuery, List<GetHotToursViewModel>>
    {
        private readonly IHotToursRepository _repository;

        public GetHotToursQueryHandler(IHotToursRepository repository) => 
            _repository = repository;

        public override async Task<IHandlerResult<List<GetHotToursViewModel>>> Handle(
            GetHotToursQuery request, 
            CancellationToken cancellationToken)
        {
            var tours = await _repository.GetAllAsync(
                tour => tour.Info.Stars == request.Stars, 
                tour => new GetHotToursViewModel
                {
                    Name = tour.Info.Name,
                    CountryName = tour.Country.Name,
                    CountryDetailsLink = tour.Country.DetailsUri.ToString(),
                    DepartureDate = tour.Departure.Date,
                    DepartureLocation = tour.Departure.Location,
                    DetailsLink = tour.Info.DetailsUri.ToString(),
                    DurationCount = tour.Duration.Count,
                    DurationType = tour.Duration.Type,
                    ImageLink = tour.Info.ImageUri.ToString(),
                    PriceAmount = tour.Price.Amount,
                    PriceType = tour.Price.Type
                });

            return Ok(tours);
        }
    }
}