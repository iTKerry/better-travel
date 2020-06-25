using System.Threading;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Abstraction.Entities;
using BetterTravel.DataAccess.EF.Seeder.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BetterTravel.DataAccess.EF.Seeder
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _dbContext;

        public DbInitializer(AppDbContext dbContext) => 
            _dbContext = dbContext;

        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.Database.EnsureDeletedAsync(cancellationToken);
            await _dbContext.Database.MigrateAsync(cancellationToken);
            await InitializeInternalAsync(cancellationToken);
        }

        private async Task InitializeInternalAsync(CancellationToken cancellationToken)
        {
            await _dbContext.Countries.AddRangeAsync(Country.AllCountries, cancellationToken);
            await _dbContext.DepartureLocations.AddRangeAsync(DepartureLocation.AllDepartures, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}