using BetterTravel.Common.Configurations;
using BetterTravel.DataAccess.EF;
using Microsoft.Extensions.DependencyInjection;

namespace BetterTravel.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBetterTravelDb(this IServiceCollection services)
        {
            var connectionString = services.BuildServiceProvider().GetService<DbConnectionString>();
            return services
                .AddTransient(sp => connectionString)
                .AddDbContext<AppDbContext>();
        }
    }
}