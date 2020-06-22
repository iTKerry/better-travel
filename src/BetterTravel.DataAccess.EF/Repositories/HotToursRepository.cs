using BetterTravel.DataAccess.Abstraction.Entities;
using BetterTravel.DataAccess.Abstraction.Repositories;

namespace BetterTravel.DataAccess.EF.Repositories
{
    public class HotToursRepository : Repository<HotTour>, IHotToursRepository
    {
        public HotToursRepository(AppDbContext db) : base(db)
        {
        }
    }
}