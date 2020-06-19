using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace BetterTravel.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRouteOptions(this IServiceCollection services) => 
            services.Configure<RouteOptions>(SetupRouteOptions);

        private static void SetupRouteOptions(RouteOptions options)
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
            options.AppendTrailingSlash = false;
        }
    }
}