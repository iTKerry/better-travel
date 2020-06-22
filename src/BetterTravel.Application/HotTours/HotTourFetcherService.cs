using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetterTravel.Application.HotTours.Abstractions;
using BetterTravel.DataAccess.Abstraction.Entities;

namespace BetterTravel.Application.HotTours
{
    public class TourFetcherService : ITourFetcherService
    {
        private readonly IEnumerable<IHotToursProvider> _hotToursProviders;

        public TourFetcherService(IEnumerable<IHotToursProvider> hotToursProviders) => 
            _hotToursProviders = hotToursProviders;

        public async Task<List<HotTour>> FetchToursAsync(int count)
        {
            var query = GetQuery(count);
            var hotToursTasks = _hotToursProviders.Select(provider => provider.GetHotToursAsync(query));
            var hotTours = await Task.WhenAll(hotToursTasks);
            return hotTours.SelectMany(t => t).ToList();
        }
        
        private static HotToursQueryObject GetQuery(int count) =>
            new HotToursQueryObject
            {
                DurationFrom = 1,
                DurationTo = 21,
                PriceFrom = 1,
                PriceTo = 50000,
                Count = count
            };
    }
}