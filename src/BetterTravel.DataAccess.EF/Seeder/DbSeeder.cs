using System.Threading;
using System.Threading.Tasks;
using BetterTravel.DataAccess.EF.Seeder.Abstractions;
using BetterTravel.DataAccess.Entities.Enumerations;
using Microsoft.EntityFrameworkCore;

namespace BetterTravel.DataAccess.EF.Seeder
{
    public class DbSeeder : IDbSeeder
    {
        private readonly AppDbContext _dbContext;

        public DbSeeder(AppDbContext dbContext) => 
            _dbContext = dbContext;

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.Database.EnsureDeletedAsync(cancellationToken);
            await _dbContext.Database.MigrateAsync(cancellationToken);
            
            await InitializeInternalAsync(cancellationToken);
        }

        private async Task InitializeInternalAsync(CancellationToken cancellationToken)
        {
            await _dbContext.Countries.AddRangeAsync(Country.AllCountries, cancellationToken);
            await _dbContext.DepartureLocations.AddRangeAsync(DepartureLocation.AllDepartures, cancellationToken);
            await _dbContext.Currencies.AddRangeAsync(Currency.AllCurrencies, cancellationToken);
            
            await _dbContext.SaveChangesAsync(true, cancellationToken);
        }
    }
}