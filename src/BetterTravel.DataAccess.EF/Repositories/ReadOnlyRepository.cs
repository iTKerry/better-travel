using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Repositories;
using BetterTravel.DataAccess.Views;
using BetterTravel.DataAccess.Views.Base;
using Microsoft.EntityFrameworkCore;

namespace BetterTravel.DataAccess.EF.Repositories
{
    public class ReadOnlyRepository<TView> : IReadOnlyRepository<TView> 
        where TView : View
    {
        protected readonly AppDbContext Ctx;

        public ReadOnlyRepository(AppDbContext db) => 
            Ctx = db;

        public async Task<List<TView>> GetAsync(QueryObject<TView> queryObject)
        {
            var query = Ctx.Set<TView>()
                .AsQueryable()
                .AsNoTracking();

            if (queryObject.WherePredicate != null)
                query = query.Where(queryObject.WherePredicate);

            if (queryObject.Skip != 0)
                query = query.Skip(queryObject.Skip);

            if (queryObject.Top != 0)
                query = query.Take(queryObject.Top);
            
            return await query.ToListAsync();
        }

        public async Task<List<TResult>> GetAsync<TResult>(QueryObject<TView, TResult> queryObject)
        {
            var query = Ctx.Set<TView>()
                .AsQueryable()
                .AsNoTracking();

            if (queryObject.WherePredicate != null)
                query = query.Where(queryObject.WherePredicate);

            var projectedQuery = query.Select(queryObject.Projection);
            
            if (queryObject.Skip != 0)
                projectedQuery = projectedQuery.Skip(queryObject.Skip);

            if (queryObject.Top != 0)
                projectedQuery = projectedQuery.Take(queryObject.Top);
            
            return await projectedQuery.ToListAsync();
        }
    }
}