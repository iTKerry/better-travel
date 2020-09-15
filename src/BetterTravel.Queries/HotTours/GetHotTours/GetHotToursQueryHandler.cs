using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BetterExtensions.Domain.Common;
using BetterExtensions.Domain.Repository;
using BetterTravel.DataAccess.Abstractions.Views;
using BetterTravel.MediatR.Core.Abstractions;
using BetterTravel.Queries.Abstractions;

namespace BetterTravel.Queries.HotTours.GetHotTours
{
    public class GetHotToursQueryHandler 
        : QueryHandlerBase<GetHotToursQuery, List<HotToursViewModel>>
    {
        private readonly IReadRepository<HotTourView> _repository;

        public GetHotToursQueryHandler(IReadRepository<HotTourView> repository) =>
            _repository = repository;

        public override async Task<IHandlerResult<List<HotToursViewModel>>> Handle(
            GetHotToursQuery request, 
            CancellationToken cancellationToken)
        {
            Expression<Func<HotTourView, HotToursViewModel>> projection = tour => new HotToursViewModel
            {
                Name = tour.Name,
                HotelCategory = tour.HotelCategory,
                DepartureDate = tour.DepartureDate,
                DepartureLocationId = tour.DepartureLocationId,
                DetailsLink = tour.DetailsLink,
                DurationCount = tour.DurationCount,
                DurationType = tour.DurationType,
                ImageLink = tour.ImageLink,
                PriceAmount = tour.PriceAmount,
                PriceType = tour.PriceType,
                PriceCurrencyId = tour.CurrencyId,
                CountryId = tour.CountryId,
                ResortName = tour.ResortName,
            };

            var projectedQueryParams = new ProjectedQueryParams<HotTourView, HotToursViewModel>
            {
                Skip = request.Skip,
                Top = request.Take,
                Projection = projection,
                WhereProjectedPredicate = request.Filter
            };

            var result = await _repository.GetAllAsync(projectedQueryParams, cancellationToken);
            var count = await _repository.CountAsync(projection, request.Filter, cancellationToken);

            return PagedData(result, count);
        }
    }
}