using BetterTravel.DataAccess.Entities;
using BetterTravel.DataAccess.Repositories;

namespace BetterTravel.DataAccess.EF.Repositories
{
    public class HotToursRepository : Repository<HotTour>, IHotToursRepository
    {
        public HotToursRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}