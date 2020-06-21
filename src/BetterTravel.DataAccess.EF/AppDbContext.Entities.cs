using BetterTravel.DataAccess.Abstraction.Entities;
using Microsoft.EntityFrameworkCore;

namespace BetterTravel.DataAccess.EF
{
    public sealed partial class AppDbContext
    {
        public DbSet<HotTour> HotTours { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
    }
}