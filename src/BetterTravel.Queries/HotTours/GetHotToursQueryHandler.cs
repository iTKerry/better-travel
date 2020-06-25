using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Abstraction.Entities;
using BetterTravel.DataAccess.Abstraction.Repositories;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using BetterTravel.Queries.Abstractions;
using BetterTravel.Queries.HotTours.Specification;
using BetterTravel.Queries.ViewModels;

namespace BetterTravel.Queries.HotTours
{
    public class GetHotToursQueryHandler : QueryHandlerBase<GetHotToursQuery, List<GetHotToursViewModel>>
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
            var queryObject = new QueryObject<HotTour, GetHotToursViewModel>
            {
                WherePredicate = _specification.ToExpression(request),
                OrderedProjection = OrderedProjection,
                Projection = Projection(),
                Skip = request.Skip,
                Top = request.Take
            };
            
            var tours = await _repository.GetAsync(queryObject);

            return Ok(tours);
        }

        private static IOrderedQueryable<GetHotToursViewModel> OrderedProjection(
            IQueryable<GetHotToursViewModel> queryableTour) =>
            queryableTour
                .OrderByDescending(tour => tour.PriceAmount)
                .ThenBy(tour => tour.CountryName)
                .ThenBy(tour => tour.DepartureLocation);
        
        private static Expression<Func<HotTour, GetHotToursViewModel>> Projection() =>
            tour => new GetHotToursViewModel
            {
                Name = tour.Info.Name,
                StarsCount = tour.Info.Stars,
                DepartureDate = tour.Info.DepartureDate,
                DepartureLocation = tour.DepartureLocation.Name,
                DetailsLink = tour.Info.DetailsUri,
                DurationCount = tour.Duration.Count,
                DurationType = tour.Duration.Type,
                ImageLink = tour.Info.ImageUri,
                PriceAmount = tour.Price.Amount,
                PriceType = tour.Price.Type,
                CountryName = tour.Country.Name,
                ResortName = tour.Resort.Name,
            };
    }
}