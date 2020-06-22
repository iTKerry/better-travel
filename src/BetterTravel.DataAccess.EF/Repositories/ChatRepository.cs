using BetterTravel.DataAccess.Abstraction.Entities;
using BetterTravel.DataAccess.Abstraction.Repositories;

namespace BetterTravel.DataAccess.EF.Repositories
{
    public class ChatRepository : Repository<Chat>, IChatRepository
    {
        public ChatRepository(AppDbContext db) : base(db)
        {
        }
    }
}