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

        public async Task<List<HotTour>> FetchToursAsync(HotToursRequestObject requestObject)
        {
            var hotToursTasks = _hotToursProviders.Select(provider => provider.GetHotToursAsync(requestObject));
            var hotTours = await Task.WhenAll(hotToursTasks);
            return hotTours.SelectMany(t => t).ToList();
        }
    }
}