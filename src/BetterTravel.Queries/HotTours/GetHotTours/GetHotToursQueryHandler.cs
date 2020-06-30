using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Common.Localization;
using BetterTravel.DataAccess.Entities;
using BetterTravel.DataAccess.Repositories;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using BetterTravel.Queries.Abstractions;
using BetterTravel.Queries.HotTours.GetHotTours.Specification;
using BetterTravel.Queries.ViewModels;

namespace BetterTravel.Queries.HotTours.GetHotTours
{
    public class GetHotToursQueryHandler 
        : QueryHandlerBase<GetHotToursQuery, List<GetHotToursViewModel>>
    {
        private readonly IHotToursRepository _repository;
        private readonly IGetHotToursSpecification _specification;

        public GetHotToursQueryHandler(
            IHotToursRepository repository, 
            IGetHotToursSpecification specification)
        {
            _repository = repository;
            _specification = specification;
        }

        public override async Task<IHandlerResult<List<GetHotToursViewModel>>> Handle(
            GetHotToursQuery request, 
            CancellationToken cancellationToken)
        {
            var queryObject = new QueryObject<HotTour>
            {
                WherePredicate = _specification.ToExpression(request),
                OrderedProjection = OrderedProjection,
                Skip = request.Skip,
                Top = request.Take
            };
            
            var tours = await _repository.GetAsync(queryObject);
            var result = tours.Select(tour => Projection(tour, request.Localize)).ToList();
            return Ok(result);
        }

        private static IOrderedQueryable<HotTour> OrderedProjection(
            IQueryable<HotTour> queryableTour) =>
            queryableTour
                .OrderByDescending(tour => tour.Price.Amount)
                .ThenBy(tour => tour.Country.Name)
                .ThenBy(tour => tour.DepartureLocation);

        private static GetHotToursViewModel Projection(HotTour tour, bool localize) =>
            new GetHotToursViewModel
            {
                Name = tour.Info.Name,
                HotelCategory = localize
                    ? L.GetValue(tour.Category.Name, Culture.Ru)
                    : tour.Category.Name,
                DepartureDate = tour.Info.DepartureDate,
                DepartureLocation = localize
                    ? L.GetValue(tour.DepartureLocation.Name, Culture.Ru)
                    : tour.DepartureLocation.Name,
                DetailsLink = tour.Info.DetailsUri,
                DurationCount = tour.Duration.Count,
                DurationType = tour.Duration.Type,
                ImageLink = tour.Info.ImageUri,
                PriceAmount = tour.Price.Amount,
                PriceType = tour.Price.Type,
                CountryName = localize
                    ? L.GetValue(tour.Country.Name, Culture.Ru)
                    : tour.Country.Name,
                ResortName = tour.Resort.Name,
            };
    }
}