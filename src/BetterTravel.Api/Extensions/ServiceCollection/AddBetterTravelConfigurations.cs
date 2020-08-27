using BetterTravel.Common.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BetterTravel.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBetterTravelConfigurations(
            this IServiceCollection services, IConfiguration cfg) =>
            services
                .Configure<ConnectionStrings>(cfg.GetSection(ConnectionStrings.Key))
                .Configure<BotConfiguration>(cfg.GetSection(BotConfiguration.Key))
                .Configure<ThirdPartyServices>(cfg.GetSection(ThirdPartyServices.Key));
    }
}