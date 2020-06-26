using System.Threading;
using System.Threading.Tasks;

namespace BetterTravel.DataAccess.EF.Seeder.Abstractions
{
    public interface IDbSeeder
    {
        Task SeedAsync(CancellationToken cancellationToken = default);
    }
}