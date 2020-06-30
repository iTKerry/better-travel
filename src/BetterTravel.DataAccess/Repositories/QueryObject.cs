using System;
using System.Linq;
using System.Linq.Expressions;
using BetterTravel.DataAccess.Entities.Base;

namespace BetterTravel.DataAccess.Repositories
{
    public sealed class QueryObject<TEntity, TProjected>
        where TEntity : Entity
    {
        public int Skip { get; set; }
        public int Top { get; set; }
        public Expression<Func<TEntity, bool>> WherePredicate { get; set; }
        public Expression<Func<TEntity, TProjected>> Projection { get; set; }
        public Func<IQueryable<TProjected>, IOrderedQueryable<TProjected>> OrderedProjection { get; set; }
    }
    
    public sealed class QueryObject<TEntity>
        where TEntity : Entity
    {
        public int Skip { get; set; }
        public int Top { get; set; }
        public Expression<Func<TEntity, bool>> WherePredicate { get; set; }
        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderedProjection { get; set; }
    }
}