using BetterTravel.DataAccess.Views;
using Microsoft.EntityFrameworkCore;

namespace BetterTravel.DataAccess.EF
{
    public sealed partial class AppDbContext
    {
        public DbSet<HotTourView> HotTourViews { get; set; }
    }
}