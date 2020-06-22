using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BetterTravel.DataAccess.EF
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=localhost,1433;Database=BetterTravel;User=SA;Password=Your_password123;");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}