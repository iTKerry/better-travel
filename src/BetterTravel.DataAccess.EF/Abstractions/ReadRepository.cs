using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BetterExtensions.Domain.Base;
using BetterExtensions.Domain.Common;
using BetterExtensions.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace BetterTravel.DataAccess.EF.Abstractions
{
    public class ReadRepository<TView> : IReadRepository<TView>
        where TView : View
    {
        protected readonly DbContext Ctx;

        public ReadRepository(DbContext db) => 
            Ctx = db;

        public async Task<List<TView>> GetAllAsync(
            QueryParams<TView> queryParams, 
            CancellationToken cancellationToken = default)
        {
            var query = Ctx.Set<TView>().AsNoTracking();

            if (queryParams.WherePredicate != null)
                query = query.Where(queryParams.WherePredicate);

            if (queryParams.Skip != 0)
                query = query.Skip(queryParams.Skip);
            
            if (queryParams.Top != 0)
                query = query.Take(queryParams.Top);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<List<TResult>> GetAllAsync<TResult>(
            ProjectedQueryParams<TView, TResult> projectedQueryParams,
            CancellationToken cancellationToken)
        {
            var query = Ctx.Set<TView>().AsNoTracking();

            var projectedQuery = query.Select(projectedQueryParams.Projection);

            if (projectedQueryParams.WhereProjectedPredicate != null)
                projectedQuery = projectedQuery.Where(projectedQueryParams.WhereProjectedPredicate);
            
            if (projectedQueryParams.Skip != 0)
                projectedQuery = projectedQuery.Skip(projectedQueryParams.Skip);

            if (projectedQueryParams.Top != 0)
                projectedQuery = projectedQuery.Take(projectedQueryParams.Top);
            
            return await projectedQuery.ToListAsync(cancellationToken);
        }

        public Task<int> CountAsync<TResult>(
            Expression<Func<TView, TResult>> projection, 
            Expression<Func<TResult, bool>> whereProjectedPredicate,
            CancellationToken cancellationToken = default)
        {
            return Ctx.Set<TView>()
                .AsNoTracking()
                .Select(projection)
                .Where(whereProjectedPredicate)
                .CountAsync(cancellationToken);
        }
    }
}