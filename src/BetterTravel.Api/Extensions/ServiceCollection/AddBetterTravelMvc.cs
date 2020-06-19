using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BetterTravel.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBetterTravelMvc(this IServiceCollection services) =>
            services
                .AddControllers()
                .AddJsonOptions(SetupJsonOptions)
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .Services;

        private static void SetupJsonOptions(JsonOptions options)
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.MaxDepth = 16;
        }
    }
}