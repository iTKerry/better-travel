using BetterTravel.DataAccess.Abstraction.Entities;
using BetterTravel.DataAccess.Abstraction.Repositories;

namespace BetterTravel.DataAccess.EF.Repositories
{
    public class ChatRepository : Repository<Chat>, IChatRepository
    {
        public ChatRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public void Save(Chat chat)
        {
            DbContext.Chats.Attach(chat);
        }
    }
}