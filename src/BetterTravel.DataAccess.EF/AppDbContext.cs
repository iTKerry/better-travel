using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Common.Configurations;
using BetterTravel.Domain.Entities;
using BetterTravel.Domain.Entities.Base;
using BetterTravel.Domain.Entities.Enumerations;
using Microsoft.EntityFrameworkCore;

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
        private readonly DbConnectionString _dbConnectionString;
        
        public AppDbContext(
            IEventDispatcher eventDispatcher, 
            DbConnectionString dbConnectionString)
        {
            _eventDispatcher = eventDispatcher;
            _dbConnectionString = dbConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(
                    _dbConnectionString.Value,
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
                .ForEach(entry =>
                {
                    _eventDispatcher.Dispatch(entry.Id, entry.DomainEvents);
                    entry.ClearDomainEvents();
                });

            return result;
        }
    }
}