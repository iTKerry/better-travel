using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Abstractions.Entities;
using BetterTravel.DataAccess.Abstractions.Repository;
using BetterTravel.DataAccess.EF.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BetterTravel.DataAccess.EF.Repositories
{
    public class ChatWriteRepository : WriteRepository<Chat>, IChatWriteRepository
    {
        public ChatWriteRepository(WriteDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Chat>> GetAllAsync() =>
            await Ctx.Chats
                .Include(c => c.Settings)
                .Include(c => c.Settings.CountrySubscriptions)
                .ThenInclude(c => c.Settings.DepartureSubscriptions)
                .ToListAsync();

        public override async Task<Chat> GetByIdAsync(int id)
        {
            var chat = await Ctx.Chats.FindAsync(id);
            return await WithDependenciesAsync(chat);
        }

        public override async Task<Chat> GetFirstAsync(Expression<Func<Chat, bool>> wherePredicate)
        {
            var chat = await Ctx.Chats.FirstOrDefaultAsync(wherePredicate);
            return await WithDependenciesAsync(chat);
        }

        private async Task<Chat> WithDependenciesAsync(Chat chat)
        {
            if (chat is null)
                return null;
            
            await Ctx.Entry(chat.Settings).Collection(c => c.CountrySubscriptions).LoadAsync();
            await Ctx.Entry(chat.Settings).Collection(c => c.DepartureSubscriptions).LoadAsync();

            return chat;
        }
    }
}