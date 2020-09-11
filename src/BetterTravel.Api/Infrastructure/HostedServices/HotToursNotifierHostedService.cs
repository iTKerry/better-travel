using System;
using System.Threading.Tasks;
using BetterExtensions.AspNet.HostedServices;
using BetterTravel.Application.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BetterTravel.Api.Infrastructure.HostedServices
{
    public class HotToursNotifierHostedService : TimedHostedService<HotToursNotifierHostedService>
    {
        protected override TimeSpan DueTime => TimeSpan.FromMinutes(1);
        protected override TimeSpan Period => TimeSpan.FromMinutes(5);
        
        public HotToursNotifierHostedService(
            ILogger<HotToursNotifierHostedService> logger, 
            IServiceScopeFactory scopeFactory) 
            : base(logger, scopeFactory)
        {
        }

        protected override async Task JobAsync(IServiceScope serviceScope) =>
            await serviceScope.ServiceProvider
                .GetRequiredService<IHotToursNotifierService>()
                .Notify();
    }
}