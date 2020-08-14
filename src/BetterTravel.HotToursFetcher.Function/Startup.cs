using System;
using BetterTravel.Common.Configurations;
using BetterTravel.Common.Constants;
using BetterTravel.DataAccess.EF;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.DataAccess.EF.Repositories;
using BetterTravel.HotToursFetcher.Function;
using BetterTravel.HotToursFetcher.Function.Abstractions;
using BetterTravel.HotToursFetcher.Function.Providers;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace BetterTravel.HotToursFetcher.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var dbConnectionString = GetDbConnectionString();
            var query = GetPoehalisnamiQuery();
            
            builder.Services
                .AddSingleton(dbConnectionString)
                .AddSingleton(query)
                .AddTransient<IPoehalisnamiProvider, PoehalisnamiProvider>()
                .AddDbContext<AppDbContext>()
                .AddTransient<IHotToursRepository, HotToursRepository>()
                .AddTransient<IChatRepository, ChatRepository>()
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<IEventDispatcher, EventDispatcher>()
                .AddTransient<MessageBus, MessageBus>()
                .AddTransient<IBus, Bus>();
        }

        private static PoehalisnamiQuery GetPoehalisnamiQuery() =>
            new PoehalisnamiQuery
            {
                DurationFrom = 1,
                DurationTo = 21,
                PriceFrom = 1,
                PriceTo = 200000,
                Count = 1000
            };

        private static DbConnectionString GetDbConnectionString() =>
            new DbConnectionString(GetEnvironmentVariable(ConnectionStrings.BetterTravelDb));

        public static string GetEnvironmentVariable(string name) =>
            Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
    }
}