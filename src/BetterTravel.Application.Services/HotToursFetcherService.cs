using System.Linq;
using System.Threading.Tasks;
using BetterTravel.Application.HotToursFetcher;
using BetterTravel.Application.HotToursFetcher.Abstractions;
using BetterTravel.Application.Services.Abstractions;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.DataAccess.Redis.Abstractions;
using Microsoft.Extensions.Logging;

namespace BetterTravel.Application.Services
{
    public class HotToursFetcherService : IHotToursFetcherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHotToursProvider _toursProvider;
        private readonly IHotTourFoundRepository _cache;
        private readonly ILogger<HotToursFetcherService> _logger;

        public HotToursFetcherService(
            IUnitOfWork unitOfWork, 
            IHotToursProvider toursProvider, 
            IHotTourFoundRepository cache, 
            ILogger<HotToursFetcherService> logger)
        {
            _unitOfWork = unitOfWork;
            _toursProvider = toursProvider;
            _cache = cache;
            _logger = logger;
        }

        public async Task FetchAndStore(HotToursQuery toursQuery)
        {
            var newTours = await _toursProvider.GetHotToursAsync(toursQuery);
            var existingTours = await _unitOfWork.HotToursRepository.GetAllAsync();

            var uniqueTours = newTours
                .Where(nTour => existingTours.All(eTour =>
                    eTour.Info.Name != nTour.Info.Name ||
                    eTour.Info.Name == nTour.Info.Name && eTour.Price.Amount != nTour.Price.Amount ||
                    eTour.Info.Name == nTour.Info.Name && eTour.DepartureLocation != nTour.DepartureLocation ||
                    eTour.Info.Name == nTour.Info.Name && eTour.DepartureDate != nTour.DepartureDate))
                .ToList();
        
            _unitOfWork.HotToursRepository.Save(uniqueTours);

            var deleteResult = await _cache.CleanAsync();
            if (deleteResult.IsFailure)
                _logger.LogCritical(deleteResult.Error);

            await _unitOfWork.CommitAsync();
        
            _logger.LogInformation($"Fetched {newTours.Count} and stored {uniqueTours.Count} tours.");
        }
    }
}