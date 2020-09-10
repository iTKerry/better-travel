using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetterExtensions.AspNet.HostedServices;
using BetterTravel.Application.Exchange.Abstractions;
using BetterTravel.Application.Services;
using BetterTravel.Application.Services.Abstractions;
using BetterTravel.Common.Localization;
using BetterTravel.Common.Utils;
using BetterTravel.DataAccess.Cache;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.DataAccess.EF.Common;
using BetterTravel.DataAccess.Entities;
using BetterTravel.DataAccess.Entities.Enumerations;
using BetterTravel.DataAccess.Redis.Abstractions;
using BetterTravel.DataAccess.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;

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