using BetterTravel.Common.Constants;
using BetterTravel.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BetterTravel.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBetterTravelDb(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(ConnectionStrings.BetterTravelDb);
            return services.AddDbContextPool<AppDbContext>(builder => 
                builder.UseSqlServer(
                    connectionString,
                    x => x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        }
    }
}