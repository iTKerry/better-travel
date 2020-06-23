using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BetterTravel.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBetterTravelMvc(this IServiceCollection services) =>
            services
                .AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.Formatting = Formatting.Indented)
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .Services;
    }
}