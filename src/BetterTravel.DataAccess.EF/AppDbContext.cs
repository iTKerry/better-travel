using System;
using System.Linq;
using BetterTravel.DataAccess.Abstraction.Entities;
using Microsoft.EntityFrameworkCore;

namespace BetterTravel.DataAccess.EF
{
    public sealed partial class AppDbContext : DbContext
    {
        private static readonly Type[] EnumerationTypes = { typeof(Country), typeof(DepartureLocation) };

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
        
        public override int SaveChanges()
        {
            var enumerationEntries = ChangeTracker.Entries()
                .Where(x => EnumerationTypes.Contains(x.Entity.GetType()));

            foreach (var enumerationEntry in enumerationEntries) 
                enumerationEntry.State = EntityState.Unchanged;

            return base.SaveChanges();
        }
    }
}