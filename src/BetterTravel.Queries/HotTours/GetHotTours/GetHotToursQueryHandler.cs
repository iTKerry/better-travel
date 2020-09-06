using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Application.Exchange.Abstractions;
using BetterTravel.Common.Localization;
using BetterTravel.DataAccess.Cache;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.DataAccess.EF.Common;
using BetterTravel.DataAccess.Entities;
using BetterTravel.DataAccess.Entities.Enumerations;
using BetterTravel.DataAccess.Enums;
using BetterTravel.DataAccess.Views;
using BetterTravel.MediatR.Core.Abstractions;
using BetterTravel.Queries.Abstractions;
using BetterTravel.Queries.ViewModels;
using CSharpFunctionalExtensions;

namespace BetterTravel.Queries.HotTours.GetHotTours
{
    public class GetHotToursQueryHandler 
        : QueryHandlerBase<GetHotToursQuery, List<GetHotToursViewModel>>
    {
        private readonly IReadOnlyRepository<HotTourView> _repository;
        private readonly IExchangeProvider _exchange;

        public GetHotToursQueryHandler(
            IReadOnlyRepository<HotTourView> repository, 
            IExchangeProvider exchange) =>
            (_repository, _exchange) = 
            (repository, exchange);

        public override async Task<IHandlerResult<List<GetHotToursViewModel>>> Handle(
            GetHotToursQuery request, 
            CancellationToken cancellationToken)
        {
            Expression<Func<HotTourView, bool>> wherePredicate = tour =>
                (!request.Countries.Any() || request.Countries.Contains(tour.CountryId)) &&
                (!request.Departures.Any() || request.Departures.Contains(tour.DepartureLocationId)) &&
                (!request.HotelCategories.Any() || request.HotelCategories.Contains(tour.HotelCategory));

            Expression<Func<HotTourView, GetHotToursViewDto>> projection = tour => new GetHotToursViewDto
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

            var queryObject = new QueryObject<HotTourView, GetHotToursViewDto>
            {
                WherePredicate = wherePredicate,
                Projection = projection,
                Skip = request.Skip,
                Top = request.Take
            };

            return await _exchange.GetExchangeAsync()
                .Map(exchanges => (Maybe<CurrencyRate>) exchanges.FirstOrDefault(ex => ex.Type == request.Currency))
                .Map(maybeExchange => GetCurrencyRate(request.Currency, maybeExchange))
                .Map(async currencyRate =>
                    (await _repository.GetAsync(queryObject))
                    .Select(tour => MapDtoToViewModel(tour, request.Localize, currencyRate))
                    .ToList())
                .Finally(result => result.IsSuccess
                    ? Ok(result.Value)
                    : ValidationFailed(result.Error));
        }

        private static (Currency Currency, double Rate) GetCurrencyRate(
            CurrencyType currencyType, Maybe<CurrencyRate> maybeExchange) =>
            maybeExchange switch
            {
                var exchange when exchange.HasValue =>
                    (Currency: Currency.FromType(maybeExchange.Value.Type), maybeExchange.Value.Rate),
                var exchange when exchange.HasNoValue =>
                    currencyType == CurrencyType.UAH
                        ? (Currency: Currency.UAH, Rate: 1)
                        : throw new ArgumentNullException(
                            $"Required currency rate was not found to exchange {currencyType.ToString()}"),
                _ => throw new InvalidOperationException()
            };

        private static GetHotToursViewModel MapDtoToViewModel(
            GetHotToursViewDto dto, bool localize, (Currency, double) currencyRate) =>
            new GetHotToursViewModel
            {
                Name = dto.Name,
                HotelCategory = localize
                    ? L.GetValue(dto.HotelCategory.ToString())
                    : dto.HotelCategory.ToString(),
                DepartureDate = dto.DepartureDate,
                DepartureLocationName = localize
                    ? L.GetValue(DepartureLocation.FromId(dto.DepartureLocationId).Name, Culture.Ru)
                    : DepartureLocation.FromId(dto.DepartureLocationId).Name,
                DetailsLink = dto.DetailsLink,
                DurationCount = dto.DurationCount,
                DurationType = dto.DurationType,
                ImageLink = dto.ImageLink,
                PriceAmount = GetPriceAmount(dto, currencyRate),
                PriceType = dto.PriceType,
                PriceCurrency = currencyRate.Item1.Name,
                CountryName = localize
                    ? L.GetValue(Country.FromId(dto.CountryId).Name, Culture.Ru)
                    : Country.FromId(dto.CountryId).Name,
                ResortName = dto.ResortName
            };

        private static double GetPriceAmount(GetHotToursViewDto dto, (Currency, double) currencyRate) =>
            Currency.FromId(dto.PriceCurrencyId) switch
            {
                var tc when tc == currencyRate.Item1 => dto.PriceAmount,
                var tc when tc == Currency.UAH => Math.Round(dto.PriceAmount / currencyRate.Item2, 2),
                _ => throw new InvalidOperationException()
            };
    }
}