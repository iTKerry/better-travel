using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Abstraction.Entities.Base;
using BetterTravel.DataAccess.Abstraction.Repositories;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace BetterTravel.DataAccess.EF.Repositories
{
    public abstract class Repository<T> : IRepository<T>
        where T : Entity
    {
        protected readonly AppDbContext DbContext;

        protected Repository(AppDbContext dbContext) => 
            DbContext = dbContext;

        public virtual async Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>> wherePredicate) =>
            await DbContext.Set<T>()
                .Where(wherePredicate)
                .ToListAsync();

        public virtual async Task<List<TResult>> GetAllAsync<TResult>(
            Expression<Func<T, bool>> wherePredicate, 
            Expression<Func<T, TResult>> projection) =>
            await DbContext.Set<T>()
                .Where(wherePredicate)
                .Select(projection)
                .ToListAsync();

        public async Task<Maybe<T>> GetByIdAsync(int id) => 
            await DbContext.Set<T>().FindAsync(id);

        public async Task<Maybe<T>> GetByAsync(Expression<Func<T, bool>> wherePredicate) => 
            await DbContext.Set<T>().FirstOrDefaultAsync(wherePredicate);

        public void Save(T chat) => 
            DbContext.Set<T>().Attach(chat);

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await DbContext.Set<T>().FindAsync(id);
            Delete(entity);
        }

        public void Delete(T entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached) 
                DbContext.Set<T>().Attach(entity);

            DbContext.Set<T>().Remove(entity);
        }
    }
}