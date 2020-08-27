using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BetterTravel.Application.HotToursFetcher.Abstractions;
using BetterTravel.Application.HotToursFetcher.Requests;
using BetterTravel.Application.HotToursFetcher.Responses;
using BetterTravel.Common.Configurations;
using BetterTravel.Common.Localization;
using BetterTravel.DataAccess.Entities;
using BetterTravel.DataAccess.Entities.Enumerations;
using BetterTravel.DataAccess.Enums;
using BetterTravel.DataAccess.ValueObjects;
using Microsoft.Extensions.Options;
using Refit;

namespace BetterTravel.Application.HotToursFetcher
{
    public class HotToursProvider : IHotToursProvider
    {
        private readonly IHotToursProviderApi _api;

        public HotToursProvider(IOptions<ThirdPartyServices> thirdPartyOptions) =>
            _api = RestService.For<IHotToursProviderApi>(thirdPartyOptions.Value.HotToursProviderUrl);

        public async Task<List<HotTour>> GetHotToursAsync(HotToursQuery query)
        {
            var request = new HotToursRequest(query);
            var response = await _api.HotTours(request);

            return response.TourListItems
                .Select(MapResponse)
                .ToList();
        }

        private static HotTour MapResponse(TourListItemResponse source) =>
            new HotTour(
                new HotTourInfo(
                    source.TourName,
                    GetDate(source.Date),
                    source.ImageUrl,
                    source.TourDetailsUrl),
                GetCategory(source.StarsCount.Count),
                new Duration(
                    source.DurationDay,
                    GetDurationType(source.NightsText)),
                new Price(
                    source.PriceFrom, 
                    GetPriceType(source.PriceDescription)),
                GetCountry(source.CountryLinks.Links.FirstOrDefault()?.Text),
                new Resort(source.ResortLinks.Links.FirstOrDefault()?.Text),
                GetDepartureLocation(source.DeparturePointNameGenitive));

        private static Country GetCountry(string source)
        {
            var name = L.GetName(source, Culture.Ru, true);
            return Country.FromName(name);
        }

        private static DepartureLocation GetDepartureLocation(string source)
        {
            var name = L.GetName(source, Culture.Ru, true);
            return DepartureLocation.FromName(name);
        }

        private static PriceType GetPriceType(string source) =>
            source switch
            {
                "за 1 человека" => PriceType.Single,
                _ => PriceType.Unknown
            };

        private static DateTime GetDate(string source)
        {
            const string pattern = "dd.MM.yyyy";
            DateTime.TryParseExact(source, pattern, null, DateTimeStyles.None, out var result);
            return result;
        }

        private static DurationType GetDurationType(string source) =>
            source switch
            {
                "ночей" => DurationType.Nights,
                _ => DurationType.Unknown
            };

        private static HotelCategory GetCategory(int count) =>
            count switch
            {
                1 => HotelCategory.HV_1,
                2 => HotelCategory.TwoStars,
                3 => HotelCategory.ThreeStars,
                4 => HotelCategory.FourStars,
                5 => HotelCategory.FiveStars,
                _ => HotelCategory.NoCategory
            };
    }
}