using BetterTravel.Common.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace BetterTravel.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBetterTravelCache(this IServiceCollection services)
        {
            var connectionString = services.BuildServiceProvider().GetService<CacheConnectionString>();
            return services.AddStackExchangeRedisCache(opt => opt.Configuration = connectionString.ToString());
        }
    }
}