using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Common.Localization;
using BetterTravel.DataAccess.Repositories;
using BetterTravel.DataAccess.Views;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using BetterTravel.Queries.Abstractions;
using BetterTravel.Queries.ViewModels;

namespace BetterTravel.Queries.HotTours.GetHotTours
{
    public class GetHotToursQueryHandler 
        : QueryHandlerBase<GetHotToursQuery, List<GetHotToursViewModel>>
    {
        private readonly IReadOnlyRepository<HotTourView> _repository;

        public GetHotToursQueryHandler(IReadOnlyRepository<HotTourView> repository) =>
            _repository = repository;

        public override async Task<IHandlerResult<List<GetHotToursViewModel>>> Handle(
            GetHotToursQuery request, 
            CancellationToken cancellationToken)
        {
            Expression<Func<HotTourView, bool>> wherePredicate = tour =>
                (!request.Countries.Any() || request.Countries.Contains(tour.Country.Id)) &&
                (!request.Departures.Any() || request.Departures.Contains(tour.DepartureLocation.Id)) &&
                (!request.HotelCategories.Any() || request.HotelCategories.Contains(tour.HotelCategory.Id));

            Expression<Func<HotTourView, GetHotToursViewModel>> projection = tour => new GetHotToursViewModel
            {
                Name = tour.Name,
                HotelCategory = tour.HotelCategory.Name,
                DepartureDate = tour.DepartureDate,
                DepartureLocationName = tour.DepartureLocation.Name,
                DetailsLink = tour.DetailsLink,
                DurationCount = tour.DurationCount,
                DurationType = tour.DurationType,
                ImageLink = tour.ImageLink,
                PriceAmount = tour.PriceAmount,
                PriceType = tour.PriceType,
                CountryName = tour.Country.Name,
                ResortName = tour.ResortName,
            };

            var queryObject = new QueryObject<HotTourView, GetHotToursViewModel>
            {
                WherePredicate = wherePredicate,
                Projection = projection,
                Skip = request.Skip,
                Top = request.Take
            };
            
            var tours = await _repository.GetAsync(queryObject);
            var result = (request.Localize ? tours.Select(LocalizeMap) : tours)
                .OrderBy(t => t.PriceAmount)
                .ThenBy(t => t.CountryName)
                .ToList();
            
            return Ok(result);
        }

        private static GetHotToursViewModel LocalizeMap(GetHotToursViewModel tour) =>
            new GetHotToursViewModel
            {
                Name = tour.Name,
                HotelCategory = L.GetValue(tour.HotelCategory),
                DepartureDate = tour.DepartureDate,
                DepartureLocationName = L.GetValue(tour.DepartureLocationName, Culture.Ru),
                DetailsLink = tour.DetailsLink,
                DurationCount = tour.DurationCount,
                DurationType = tour.DurationType,
                ImageLink = tour.ImageLink,
                PriceAmount = tour.PriceAmount,
                PriceType = tour.PriceType,
                CountryName = L.GetValue(tour.CountryName, Culture.Ru),
                ResortName = tour.ResortName,
            };
    }
}