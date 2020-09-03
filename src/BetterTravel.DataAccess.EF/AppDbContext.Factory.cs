﻿using BetterTravel.Common.Configurations;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace BetterTravel.DataAccess.EF
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        private const string ConnectionString =
            "Server=localhost,1433;Database=BetterTravel;User=SA;Password=Your_password123;";
        
        public AppDbContext CreateDbContext(string[] args)
        {
            var connectionStrings = new ConnectionStrings {BetterTravelDb = ConnectionString};

            return new AppDbContext(null, new OptionsWrapper<ConnectionStrings>(connectionStrings));
        }
    }
}