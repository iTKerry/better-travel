using BetterTravel.DataAccess.EF;
using Microsoft.Extensions.DependencyInjection;

namespace BetterTravel.Api.Extensions.ServiceCollection
{
    public static partial class AddDevChallengeHealthChecks
    {
        public static IServiceCollection AddBetterTravelHealthChecks(this IServiceCollection services) =>
            services
                .AddHealthChecks()
                .AddDbContextCheck<AppDbContext>()
                .Services;
    }
}