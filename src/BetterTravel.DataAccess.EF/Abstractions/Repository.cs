using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BetterTravel.DataAccess.EF.Common;
using BetterTravel.DataAccess.Entities.Base;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace BetterTravel.DataAccess.EF.Abstractions
{
    public abstract class Repository<T> : IRepository<T>
        where T : AggregateRoot
    {
        protected readonly AppDbContext Ctx;

        protected Repository(AppDbContext dbContext) => 
            Ctx = dbContext;

        public virtual async Task<List<T>> GetAsync(ProjectedQueryParams<T> projectedQueryParams)
        {
            if (projectedQueryParams is null)
                throw new ArgumentNullException(nameof(projectedQueryParams));

            var query = Ctx.Set<T>().AsQueryable();

            if (projectedQueryParams.WherePredicate != null)
                query = query.Where(projectedQueryParams.WherePredicate);

            if (projectedQueryParams.Skip != 0)
                query = query.Skip(projectedQueryParams.Skip);

            if (projectedQueryParams.Top != 0)
                query = query.Take(projectedQueryParams.Top);
            
            return await query.ToListAsync();
        }
        
        public virtual async Task<List<TResult>> GetAsync<TResult>(ProjectedQueryParams<T, TResult> projectedQueryParams)
        {
            if (projectedQueryParams is null)
                throw new ArgumentNullException(nameof(projectedQueryParams));
            
            var query = Ctx.Set<T>().AsQueryable();

            if (projectedQueryParams.WherePredicate != null)
                query = query.Where(projectedQueryParams.WherePredicate);

            var projectedQuery = query.Select(projectedQueryParams.Projection);
            
            if (projectedQueryParams.Skip != 0)
                projectedQuery = projectedQuery.Skip(projectedQueryParams.Skip);

            if (projectedQueryParams.Top != 0)
                projectedQuery = projectedQuery.Take(projectedQueryParams.Top);
            
            return await projectedQuery.ToListAsync();
        }

        public virtual async Task<Maybe<T>> GetByIdAsync(int id) => 
            await Ctx.Set<T>().FindAsync(id);

        public virtual async Task<Maybe<T>> GetFirstAsync(Expression<Func<T, bool>> wherePredicate) => 
            await Ctx.Set<T>().FirstOrDefaultAsync(wherePredicate);

        public virtual void Save(T chat) => 
            Ctx.Set<T>().Attach(chat);
        
        public virtual void Save(List<T> chats) => 
            Ctx.Set<T>().AttachRange(chats);

        public virtual async Task DeleteByIdAsync(int id)
        {
            var entity = await Ctx.Set<T>().FindAsync(id);
            Delete(entity);
        }

        public virtual void Delete(T entity)
        {
            if (Ctx.Entry(entity).State == EntityState.Detached) 
                Ctx.Set<T>().Attach(entity);

            Ctx.Set<T>().Remove(entity);
        }
    }
}