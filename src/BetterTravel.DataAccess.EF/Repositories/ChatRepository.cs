using BetterTravel.DataAccess.Entities;
using BetterTravel.DataAccess.Repositories;

namespace BetterTravel.DataAccess.EF.Repositories
{
    public class ChatRepository : Repository<Chat>, IChatRepository
    {
        public ChatRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}