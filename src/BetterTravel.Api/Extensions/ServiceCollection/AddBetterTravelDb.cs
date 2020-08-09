using BetterTravel.Common.Configurations;
using BetterTravel.Common.Constants;
using BetterTravel.DataAccess.EF;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BetterTravel.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBetterTravelDb(this IServiceCollection services, 
            IConfiguration cfg)
        {
            var dbConnectionString = new DbConnectionString(cfg.GetConnectionString(ConnectionStrings.BetterTravelDb));
            
            return services
                .AddTransient(sp => dbConnectionString)
                .AddDbContext<AppDbContext>();
        }
    }
}