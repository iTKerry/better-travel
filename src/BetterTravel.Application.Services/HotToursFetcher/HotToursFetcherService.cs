using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BetterExtensions.Domain.Common;
using BetterExtensions.Domain.Repository;
using BetterTravel.Application.HotToursFetcher;
using BetterTravel.Application.HotToursFetcher.Abstractions;
using BetterTravel.Application.Services.Abstractions;
using BetterTravel.DataAccess.Abstractions.Repository;
using BetterTravel.DataAccess.Abstractions.Views;
using BetterTravel.DataAccess.Redis.Abstractions;
using Microsoft.Extensions.Logging;

namespace BetterTravel.Application.Services.HotToursFetcher
{
    public class HotToursFetcherService : IHotToursFetcherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHotToursProvider _toursProvider;
        private readonly IHotTourFoundRepository _cache;
        private readonly ILogger<HotToursFetcherService> _logger;
        private readonly IReadRepository<HotTourView> _repository;

        public HotToursFetcherService(
            IUnitOfWork unitOfWork, 
            IHotToursProvider toursProvider, 
            IHotTourFoundRepository cache,
            IReadRepository<HotTourView> repository,
            ILogger<HotToursFetcherService> logger)
        {
            _unitOfWork = unitOfWork;
            _toursProvider = toursProvider;
            _cache = cache;
            _repository = repository;
            _logger = logger;
        }

        public async Task FetchAndStore()
        {
            Expression<Func<HotTourView, HotTourViewDto>> projection = hotTourView => new HotTourViewDto
            {
                Name = hotTourView.Name,
                PriceAmount = hotTourView.PriceAmount,
                DepartureLocationId = hotTourView.DepartureLocationId,
                DepartureDate = hotTourView.DepartureDate
            };
            
            var projectedQueryParams = new ProjectedQueryParams<HotTourView, HotTourViewDto>
            {
                Projection = projection
            };
            
            var providerQuery = new HotToursProviderQuery
            {
                DurationFrom = 1,
                DurationTo = 21,
                PriceFrom = 1,
                PriceTo = 200000,
                Count = 1000
            };

            var newTours = await _toursProvider.GetHotToursAsync(providerQuery);
            var existingTours = await _repository.GetAllAsync(projectedQueryParams);

            var uniqueTours = newTours
                .Where(n => existingTours.All(e =>
                    e.Name != n.Info.Name ||
                    e.Name == n.Info.Name && e.PriceAmount != n.Price.Amount ||
                    e.Name == n.Info.Name && e.DepartureLocationId != n.DepartureLocation.Id ||
                    e.Name == n.Info.Name && e.DepartureDate != n.DepartureDate))
                .ToList();
        
            _unitOfWork.HotToursWriteRepository.SaveRange(uniqueTours);

            var deleteResult = await _cache.CleanAsync();
            if (deleteResult.IsFailure)
                _logger.LogCritical(deleteResult.Error);

            await _unitOfWork.CommitAsync();
        
            _logger.LogInformation($"Fetched {newTours.Count} and stored {uniqueTours.Count} tours.");
        }
    }
}