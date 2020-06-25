using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using BetterTravel.Application.HotTours.Abstractions;
using BetterTravel.Application.HotTours.Providers.Poehalisnami.Responses;
using BetterTravel.Common.Localization;
using BetterTravel.DataAccess.Abstraction.Entities;
using BetterTravel.DataAccess.Abstraction.Entities.Enums;
using BetterTravel.DataAccess.Abstraction.ValueObjects;
using Refit;

namespace BetterTravel.Application.HotTours.Providers.Poehalisnami
{
    public class PoehalisnamiProvider : IHotToursProvider
    {
        private readonly IPoehalisnamiApi _api;

        public PoehalisnamiProvider() => 
            _api = RestService.For<IPoehalisnamiApi>("https://www.poehalisnami.ua");

        public async Task<List<HotTour>> GetHotToursAsync(HotToursQueryObject queryObject)
        {
            var request = MapRequest(queryObject);
            var response = await _api.HotTours(request);

            return response.TourListItems
                .Select(MapResponse)
                .ToList();
        }

        private static PoehalisnamiRequest MapRequest(HotToursQueryObject queryObject) =>
            new PoehalisnamiRequest
            {
                CountryIds = string.Empty,
                ResortIds = string.Empty,
                DepartureIds = string.Empty,
                HotelCategoryIds = string.Empty,
                BoardIds = string.Empty,
                RestTypeIds = string.Empty,
                DateFrom = DateTime.Now.ToString("dd-MM-yyyy"),
                DateTo = DateTime.Now.AddYears(1).ToString("dd-MM-yyyy"),
                DurationQtyDayFrom = queryObject.DurationFrom,
                DurationQtyDayTo = queryObject.DurationTo,
                PriceFrom = queryObject.PriceFrom,
                PriceTo = queryObject.PriceTo,
                LanguageId = 2,
                CurrentPage = 1,
                PageSize = queryObject.Count,
                SortId = 0
            };

        private static HotTour MapResponse(TourListItemResponse source) =>
            new HotTour(
                new HotTourInfo(
                    source.TourName,
                    GetStars(source.StarsCount.Count),
                    GetDate(source.Date),
                    source.ImageUrl,
                    source.TourDetailsUrl),
                new Duration(
                    source.DurationDay,
                    GetDurationType(source.NightsText)),
                new Price(
                    source.PriceFrom, 
                    GetPriceType(source.PriceDescription)),
                GetCountry(source.CountryLinks.Links.FirstOrDefault()?.Text),
                new Resort(
                    source.ResortLinks.Links.FirstOrDefault()?.Text,
                    source.ResortLinks.Links.FirstOrDefault()?.Href),
                GetDepartureLocation(source.DeparturePointNameGenitive));

        private static Country GetCountry(string source)
        {
            var temp = Localization.ResourceManager.GetResourceName(source, new CultureInfo("ru-RU"), true);
            Console.WriteLine(temp);
            
            return null;
        }

        private static DepartureLocation GetDepartureLocation(string source)
        {
            return null;
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

        private static Stars GetStars(int count) =>
            count switch
            {
                1 => Stars.One,
                2 => Stars.Two,
                3 => Stars.Three,
                4 => Stars.Four,
                5 => Stars.Five,
                _ => Stars.Unknown
            };
    }
}