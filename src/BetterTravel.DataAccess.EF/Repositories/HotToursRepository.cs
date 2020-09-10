using System.Collections.Generic;
using System.Threading.Tasks;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BetterTravel.DataAccess.EF.Repositories
{
    public class HotToursRepository : Repository<HotTour>, IHotToursRepository
    {
        public HotToursRepository(AppDbContext dbContext) 
            : base(dbContext)
        {
        }

        public async Task<List<HotTour>> GetAllAsync() =>
            await Ctx.HotTours
                .AsNoTracking()
                .Include(t => t.Country)
                .Include(t => t.DepartureLocation)
                .ToListAsync();

        public override void Save(List<HotTour> chats)
        {
            base.Save(chats);
            chats.ForEach(chat => chat.NotifyFound());
        }
    }
}