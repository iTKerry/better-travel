using Microsoft.Extensions.DependencyInjection;

namespace BetterTravel.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBetterTravelProfiler(this IServiceCollection services) =>
            services
                .AddMiniProfiler()
                .AddEntityFramework()
                .Services;
    }
}