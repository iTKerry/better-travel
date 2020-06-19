using BetterTravel.Common.Constants;
using BetterTravel.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BetterTravel.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDevChallengeDb(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(ConnectionStrings.BetterTravelDb);
            return services.AddDbContextPool<AppDbContext>(x => x.UseSqlServer(connectionString));
        }
    }
}