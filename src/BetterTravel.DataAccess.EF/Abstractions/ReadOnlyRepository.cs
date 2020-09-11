using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.DataAccess.EF.Common;
using BetterTravel.DataAccess.Views.Base;
using Microsoft.EntityFrameworkCore;

namespace BetterTravel.DataAccess.EF.Abstractions
{
    public class ReadOnlyRepository<TView> : IReadOnlyRepository<TView> 
        where TView : View
    {
        protected readonly AppDbContext Ctx;

        public ReadOnlyRepository(AppDbContext db) => 
            Ctx = db;

        public async Task<List<TView>> GetAllAsync(
            ProjectedQueryParams<TView> projectedQueryParams,
            CancellationToken cancellationToken)
        {
            var query = Ctx.Set<TView>().AsNoTracking();

            if (projectedQueryParams.WherePredicate != null)
                query = query.Where(projectedQueryParams.WherePredicate);

            if (projectedQueryParams.Skip != 0)
                query = query.Skip(projectedQueryParams.Skip);

            if (projectedQueryParams.Top != 0)
                query = query.Take(projectedQueryParams.Top);
            
            return await query.ToListAsync(cancellationToken);
        }

        public async Task<List<TResult>> GetAllAsync<TResult>(
            ProjectedQueryParams<TView, TResult> projectedQueryParams,
            CancellationToken cancellationToken)
        {
            var query = Ctx.Set<TView>().AsNoTracking();

            if (projectedQueryParams.WherePredicate != null)
                query = query.Where(projectedQueryParams.WherePredicate);

            var projectedQuery = query.Select(projectedQueryParams.Projection);
            
            if (projectedQueryParams.Skip != 0)
                projectedQuery = projectedQuery.Skip(projectedQueryParams.Skip);

            if (projectedQueryParams.Top != 0)
                projectedQuery = projectedQuery.Take(projectedQueryParams.Top);
            
            return await projectedQuery.ToListAsync(cancellationToken);
        }
    }
}