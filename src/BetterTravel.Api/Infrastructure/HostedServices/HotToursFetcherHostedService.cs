using System;
using System.Linq;
using System.Threading.Tasks;
using BetterExtensions.AspNet.HostedServices;
using BetterTravel.Application.HotToursFetcher;
using BetterTravel.Application.HotToursFetcher.Abstractions;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.DataAccess.EF.Common;
using BetterTravel.DataAccess.Entities;
using BetterTravel.DataAccess.Redis.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BetterTravel.Api.Infrastructure.HostedServices
{
    public class HotToursFetcherHostedService : TimedHostedService<HotToursFetcherHostedService>
    {
        protected override TimeSpan Period => TimeSpan.FromMinutes(5);

        public HotToursFetcherHostedService(
            IServiceProvider services, 
            ILogger<HotToursFetcherHostedService> logger) 
            : base(services, logger) { }

        protected override async Task JobAsync(IServiceScope scope)
        {
            var provider = scope.ServiceProvider.GetRequiredService<IHotToursProvider>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var cache = scope.ServiceProvider.GetRequiredService<IHotTourFoundRepository>();
            
            var query = new HotToursQuery
            {
                DurationFrom = 1,
                DurationTo = 21,
                PriceFrom = 1,
                PriceTo = 200000,
                Count = 1000
            };

            var newTours = await provider.GetHotToursAsync(query);
            var existingTours = await unitOfWork.HotToursRepository.GetAsync(new QueryObject<HotTour>());

            var uniqueTours = newTours
                .Where(nTour => existingTours.All(eTour =>
                    eTour.Info.Name != nTour.Info.Name ||
                    eTour.Info.Name == nTour.Info.Name && eTour.Price.Amount != nTour.Price.Amount ||
                    eTour.Info.Name == nTour.Info.Name && eTour.DepartureLocation != nTour.DepartureLocation ||
                    eTour.Info.Name == nTour.Info.Name && eTour.DepartureDate != nTour.DepartureDate))
                .ToList();
        
            unitOfWork.HotToursRepository.Save(uniqueTours);

            var deleteResult = await cache.CleanAsync();
            if (deleteResult.IsFailure)
                _logger.LogCritical(deleteResult.Error);

            await unitOfWork.CommitAsync();
        
            _logger.LogInformation($"Fetched {newTours.Count} and stored {uniqueTours.Count} tours.");
        }
    }
}