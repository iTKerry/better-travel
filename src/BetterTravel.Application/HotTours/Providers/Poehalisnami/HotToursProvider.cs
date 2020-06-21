using System;
using System.Threading.Tasks;
using BetterTravel.Application.HotTours.Abstractions;
using BetterTravel.DataAccess.Abstraction.Entities;
using Refit;

namespace BetterTravel.Application.HotTours.Providers.Poehalisnami
{
    public class PoehalisnamiProvider : IHotToursProvider
    {
        private readonly IPoehalisnamiApi _api;

        public PoehalisnamiProvider() => 
            _api = RestService.For<IPoehalisnamiApi>("https://www.poehalisnami.ua");

        public async Task<HotTour> GetHotToursAsync(HotToursQueryObject queryObject)
        {
            var request = GetRequest(queryObject);
            var response = await _api.HotTours(request);
            
            throw new NotImplementedException();
        }

        private static PoehalisnamiRequest GetRequest(HotToursQueryObject queryObject) =>
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
        
    }
}