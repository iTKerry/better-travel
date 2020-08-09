using System.Collections.Generic;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.Domain.Entities;

namespace BetterTravel.DataAccess.EF.Repositories
{
    public class HotToursRepository : Repository<HotTour>, IHotToursRepository
    {
        public HotToursRepository(AppDbContext dbContext) 
            : base(dbContext)
        {
        }

        public override void Save(List<HotTour> chats)
        {
            base.Save(chats);
            chats.ForEach(chat => chat.NotifyFound());
        }
    }
}