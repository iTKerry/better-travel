using BetterTravel.DataAccess.Entities;
using BetterTravel.DataAccess.Entities.Enumerations;
using Microsoft.EntityFrameworkCore;

namespace BetterTravel.DataAccess.EF
{
    public sealed partial class AppDbContext
    {
        public DbSet<HotTour> HotTours { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<DepartureLocation> DepartureLocations { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}