using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BetterTravel.Api.Extensions.Host
{
    public static class HostExtensions
    {
        public static async Task<IHost> MigrateDatabaseAsync<T>(this IHost host) 
            where T : DbContext
        {
            using var scope = host.Services.CreateScope();
            var appDbContext = scope.ServiceProvider.GetRequiredService<T>();

            await appDbContext.Database.EnsureDeletedAsync();
            await appDbContext.Database.MigrateAsync();

            return host;
        }
    }
}