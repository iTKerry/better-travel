using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.DataAccess.EF.Repositories;
using BetterTravel.Domain.Entities;
using BetterTravel.Poehalisnami.TimerFunction.Abstractions;
using BetterTravel.Poehalisnami.TimerFunction.Requests;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace BetterTravel.Poehalisnami.TimerFunction
{
    public class TourFetcherFunction
    {
        private readonly IPoehalisnamiProvider _provider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PoehalisnamiQuery _query;

        public TourFetcherFunction(
            IPoehalisnamiProvider service, 
            IUnitOfWork unitOfWork, 
            PoehalisnamiQuery query) =>
            (_provider, _unitOfWork, _query) = 
            (service, unitOfWork, query);

        [FunctionName(nameof(TourFetcherFunction))]
        public async Task RunAsync(
            [TimerTrigger("0 */5 * * * *", RunOnStartup = true)] TimerInfo myTimer, 
            ILogger log)
        {
            var newTours = await _provider.GetHotToursAsync(_query);
            
            var newToursNames = newTours
                .Select(q => q.Info.Name)
                .ToList();

            var queryObject = new QueryObject<HotTour>
            {
                WherePredicate = cachedTour => 
                    newToursNames.Any(t => t == cachedTour.Info.Name)
            };
            
            var cachedTours = await _unitOfWork.HotToursRepository.GetAsync(queryObject);
            
            var uniqueTours = newTours
                .Where(newTour =>
                    cachedTours.All(cachedTour =>
                        cachedTour.Info.Name != newTour.Info.Name &&
                        cachedTour.Price.Amount != newTour.Price.Amount &&
                        cachedTour.Category.Id != newTour.Category.Id))
                .ToList();
        
            _unitOfWork.HotToursRepository.Save(uniqueTours);
            
            await _unitOfWork.CommitAsync();
        
            log.LogInformation($"Fetched {newTours.Count} and stored {uniqueTours.Count} tours.");
        }
    }
}