using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BetterExtensions.Domain.Base;
using BetterExtensions.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace BetterTravel.DataAccess.EF.Abstractions
{
    public abstract class WriteRepository<T> : IWriteRepository<T>
        where T : AggregateRoot
    {
        protected readonly WriteDbContext Ctx;

        protected WriteRepository(WriteDbContext dbContext) => 
            Ctx = dbContext;

        public virtual async Task<T> GetFirstAsync(Expression<Func<T, bool>> wherePredicate) => 
            await Ctx.Set<T>().FirstOrDefaultAsync(wherePredicate);

        public virtual async Task<T> GetByIdAsync(int id) => 
            await Ctx.Set<T>().FindAsync(id);

        public virtual void Save(T chat) => 
            Ctx.Set<T>().Attach(chat);

        public virtual void SaveRange(List<T> chats) => 
            Ctx.Set<T>().AttachRange(chats);

        public virtual void Delete(T entity)
        {
            if (Ctx.Entry(entity).State == EntityState.Detached) 
                Ctx.Set<T>().Attach(entity);

            Ctx.Set<T>().Remove(entity);
        }
    }
}