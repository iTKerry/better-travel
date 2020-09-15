using BetterTravel.Common.Configurations;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace BetterTravel.DataAccess.EF.Common
{
    public class MigrationsFactory : IDesignTimeDbContextFactory<WriteDbContext>
    {
        private const string ConnectionString =
            "Server=localhost,1433;Database=BetterTravel;User=SA;Password=Your_password123;";
        
        public WriteDbContext CreateDbContext(string[] args)
        {
            var connectionStrings = new ConnectionStrings {BetterTravelDb = ConnectionString};

            return new WriteDbContext(null, new OptionsWrapper<ConnectionStrings>(connectionStrings), null, null);
        }
    }
}