using System;
using System.Threading.Tasks;
using BetterExtensions.AspNet.HostedServices;
using BetterTravel.Application.HotToursFetcher;
using BetterTravel.Application.Services;
using BetterTravel.Application.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BetterTravel.Api.Infrastructure.HostedServices
{
    public class HotToursFetcherHostedService : TimedHostedService<HotToursFetcherHostedService>
    {
        protected override TimeSpan Period => TimeSpan.FromMinutes(5);

        public HotToursFetcherHostedService(
            ILogger<HotToursFetcherHostedService> logger,
            IServiceScopeFactory scopeFactory)
            : base(logger, scopeFactory)
        {
        }

        protected override async Task JobAsync(IServiceScope scope) =>
            await scope.ServiceProvider
                .GetRequiredService<IHotToursFetcherService>()
                .FetchAndStore(new HotToursQuery
                {
                    DurationFrom = 1,
                    DurationTo = 21,
                    PriceFrom = 1,
                    PriceTo = 200000,
                    Count = 1000
                });
    }
}