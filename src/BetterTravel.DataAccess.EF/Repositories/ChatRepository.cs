using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Entities;
using BetterTravel.DataAccess.Repositories;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace BetterTravel.DataAccess.EF.Repositories
{
    public class ChatRepository : Repository<Chat>, IChatRepository
    {
        public ChatRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Maybe<Chat>> GetByIdAsync(int id)
        {
            var chat = await DbContext.Chats.FindAsync(id);
            return await WithDependenciesAsync(chat);
        }

        public override async Task<Maybe<Chat>> GetFirstAsync(Expression<Func<Chat, bool>> wherePredicate)
        {
            var chat = await DbContext.Chats.FirstOrDefaultAsync(wherePredicate);
            return await WithDependenciesAsync(chat);
        }

        private async Task<Maybe<Chat>> WithDependenciesAsync(Chat chat)
        {
            if (chat is null)
                return null;
            
            await DbContext.Entry(chat.Settings).Collection(c => c.CountrySubscriptions).LoadAsync();
            await DbContext.Entry(chat.Settings).Collection(c => c.DepartureSubscriptions).LoadAsync();

            return chat;
        }
    }
}