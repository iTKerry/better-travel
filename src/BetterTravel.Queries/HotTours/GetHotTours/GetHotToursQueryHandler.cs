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
                PriceCurrency = tour.Currency.Name,
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

            return await _exchange.GetExchangeAsync()
                .Map(exchanges => (Maybe<CurrencyRate>) exchanges.FirstOrDefault(ex => ex.Type == request.Currency))
                .Map(maybeExchange => GetCurrencyRate(request.Currency, maybeExchange))
                .Map(async currencyRate =>
                    (await _repository.GetAsync(queryObject))
                    .Select(tour => Map(tour, request.Localize, currencyRate))
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

        private static GetHotToursViewModel Map(GetHotToursViewModel tour, bool localize,
            (Currency, double) currencyRate) =>
            new GetHotToursViewModel
            {
                Name = tour.Name,
                HotelCategory = localize
                    ? L.GetValue(tour.HotelCategory)
                    : tour.HotelCategory,
                DepartureDate = tour.DepartureDate,
                DepartureLocationName = localize
                    ? L.GetValue(tour.DepartureLocationName, Culture.Ru)
                    : tour.DepartureLocationName,
                DetailsLink = tour.DetailsLink,
                DurationCount = tour.DurationCount,
                DurationType = tour.DurationType,
                ImageLink = tour.ImageLink,
                PriceAmount = GetPriceAmount(tour, currencyRate),
                PriceType = tour.PriceType,
                PriceCurrency = currencyRate.Item1.Name,
                CountryName = localize
                    ? L.GetValue(tour.CountryName, Culture.Ru)
                    : tour.CountryName,
                ResortName = tour.ResortName,
            };

        private static double GetPriceAmount(GetHotToursViewModel tour, (Currency, double) currencyRate) =>
            Currency.FromName(tour.PriceCurrency) switch
            {
                var tc when tc == currencyRate.Item1 => tour.PriceAmount,
                var tc when tc == Currency.UAH => Math.Round(tour.PriceAmount / currencyRate.Item2, 2),
                _ => throw new InvalidOperationException()
            };
    }
}