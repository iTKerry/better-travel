using BetterTravel.Api.Extensions.Configuration;
using BetterTravel.Common.Configurations;
using BetterTravel.Common.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BetterTravel.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBetterTravelConfigurations(
            this IServiceCollection services,
            IConfiguration cfg)
        {
            var cacheConnectionString =
                new CacheConnectionString(cfg.GetConnectionString(ConnectionStrings.BetterTravelCache));
            
            var dbConnectionString = 
                new DbConnectionString(cfg.GetConnectionString(ConnectionStrings.BetterTravelDb));

            var botConfiguration = cfg.GetOptions<BotConfiguration>(nameof(BotConfiguration));

            var hotToursProviderUri = new HotToursProviderUri(cfg.GetSection(nameof(HotToursProviderUri)).Value);
            
            return services
                .AddSingleton(cacheConnectionString)
                .AddSingleton(dbConnectionString)
                .AddSingleton(botConfiguration)
                .AddSingleton(hotToursProviderUri);
        }
    }
}