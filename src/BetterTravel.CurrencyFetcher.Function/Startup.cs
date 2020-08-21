using BetterTravel.CurrencyFetcher.Function;
using BetterTravel.DataAccess.Redis;
using BetterTravel.DataAccess.Redis.Base;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace BetterTravel.CurrencyFetcher.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddTransient<IExchangeProvider, ExchangeProvider>()
                .AddTransient(typeof(CacheProvider<>), typeof(CurrencyRateCacheProvider))
                .AddStackExchangeRedisCache(options => options.Configuration = "localhost");
        }
    }
}