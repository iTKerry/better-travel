using BetterTravel.Common.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BetterTravel.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBetterTravelCache(this IServiceCollection services, IConfiguration cfg)
        {
            var connectionString = cfg.GetConnectionString(ConnectionStrings.BetterTravelCache);
            return services.AddStackExchangeRedisCache(opt => opt.Configuration = connectionString);
        }
    }
}