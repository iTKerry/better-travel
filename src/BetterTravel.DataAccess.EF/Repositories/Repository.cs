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
        where T : AggregateRoot
    {
        protected readonly AppDbContext DbContext;

        protected Repository(AppDbContext dbContext) => 
            DbContext = dbContext;

        public virtual async Task<List<T>> GetAsync(QueryObject<T> queryObject)
        {
            var query = DbContext.Set<T>().AsQueryable();

            if (queryObject.WherePredicate != null)
                query = query.Where(queryObject.WherePredicate);

            if (queryObject.OrderedProjection != null)
                query = queryObject.OrderedProjection(query);
            
            if (queryObject.Skip != 0)
                query = query.Skip(queryObject.Skip);

            if (queryObject.Top != 0)
                query = query.Take(queryObject.Top);
            
            return await query.ToListAsync();
        }
        
        public virtual async Task<List<TResult>> GetAsync<TResult>(QueryObject<T, TResult> queryObject)
        {
            var query = DbContext.Set<T>().AsQueryable();

            if (queryObject.WherePredicate != null)
                query = query.Where(queryObject.WherePredicate);

            var projectedQuery = query.Select(queryObject.Projection);
            
            if (queryObject.OrderedProjection != null)
                projectedQuery = queryObject.OrderedProjection(projectedQuery);

            if (queryObject.Skip != 0)
                projectedQuery = projectedQuery.Skip(queryObject.Skip);

            if (queryObject.Top != 0)
                projectedQuery = projectedQuery.Take(queryObject.Top);
            
            return await projectedQuery.ToListAsync();
        }

        public virtual async Task<Maybe<T>> GetByIdAsync(int id) => 
            await DbContext.Set<T>().FindAsync(id);

        public virtual async Task<Maybe<T>> GetFirstAsync(Expression<Func<T, bool>> wherePredicate) => 
            await DbContext.Set<T>().FirstOrDefaultAsync(wherePredicate);

        public virtual void Save(T chat) => 
            DbContext.Set<T>().Attach(chat);
        
        public virtual async Task SaveAsync(List<T> chats) => 
            await DbContext.Set<T>().AddRangeAsync(chats);

        public virtual async Task DeleteByIdAsync(int id)
        {
            var entity = await DbContext.Set<T>().FindAsync(id);
            Delete(entity);
        }

        public virtual void Delete(T entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached) 
                DbContext.Set<T>().Attach(entity);

            DbContext.Set<T>().Remove(entity);
        }
    }
}