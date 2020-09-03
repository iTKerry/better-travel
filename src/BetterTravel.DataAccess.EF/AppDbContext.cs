using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Common.Configurations;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.DataAccess.EF.Common;
using BetterTravel.DataAccess.Entities;
using BetterTravel.DataAccess.Entities.Base;
using BetterTravel.DataAccess.Entities.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BetterTravel.DataAccess.EF
{
    public sealed partial class AppDbContext : DbContext
    {
        private static readonly Type[] EnumerationTypes =
        {
            typeof(Country), 
            typeof(DepartureLocation),
            typeof(Currency)
        };

        private readonly IEventDispatcher _eventDispatcher;
        private readonly string _dbConnectionString;

        public AppDbContext(
            IEventDispatcher eventDispatcher,
            IOptions<ConnectionStrings> connectionStringsOptions)
        {
            _eventDispatcher = eventDispatcher;
            _dbConnectionString = connectionStringsOptions.Value.BetterTravelDb;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(
                    _dbConnectionString,
                    x => x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.Entries()
                .Where(x => EnumerationTypes.Contains(x.Entity.GetType()))
                .ToList()
                .ForEach(entity => entity.State = EntityState.Unchanged);

            var changedEntries = ChangeTracker
                .Entries()
                .Where(x => x.Entity is Entity)
                .Select(x => (Entity) x.Entity)
                .ToList();

            var result = await base.SaveChangesAsync(cancellationToken);

            changedEntries
                .OfType<AggregateRoot>()
                .ToList()
                .ForEach(async entry =>
                {
                    await _eventDispatcher.DispatchAsync(entry.Id, entry.DomainEvents);
                    entry.ClearDomainEvents();
                });

            return result;
        }
    }
}