using BetterTravel.Common.Configurations;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BetterTravel.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBetterTravelCache(this IServiceCollection services)
        {
            services
                .AddOptions<RedisCacheOptions>()
                .Configure<IOptions<ConnectionStrings>>((redis, opt) =>
                    redis.Configuration = opt.Value.BetterTravelCache);
            
            return services.AddStackExchangeRedisCache(opt => {});
        }
    }
}