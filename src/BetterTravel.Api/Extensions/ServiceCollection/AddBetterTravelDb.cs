using BetterTravel.DataAccess.EF;
using Microsoft.Extensions.DependencyInjection;

namespace BetterTravel.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBetterTravelDb(this IServiceCollection services) => 
            services
                .AddDbContext<WriteDbContext>()
                .AddDbContext<ReadDbContext>();
    }
}