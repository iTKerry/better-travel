using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Abstractions.Entities;
using BetterTravel.DataAccess.Abstractions.Repository;
using BetterTravel.DataAccess.EF.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BetterTravel.DataAccess.EF.Repositories
{
    public class HotToursWriteRepository 
        : WriteRepository<HotTour>, IHotToursWriteRepository
    {
        public HotToursWriteRepository(WriteDbContext dbContext) 
            : base(dbContext)
        {
        }

        public async Task<List<HotTour>> GetAllAsync(
            Expression<Func<HotTour, bool>> predicate, 
            CancellationToken cancellationToken = default)
        {
            return await Ctx.HotTours
                .AsNoTracking()
                .Where(predicate)
                .Include(t => t.Country)
                .Include(t => t.DepartureLocation)
                .ToListAsync(cancellationToken);
        }

        public override void SaveRange(List<HotTour> chats)
        {
            base.SaveRange(chats);
            chats.ForEach(chat => chat.NotifyFound());
        }
    }
}