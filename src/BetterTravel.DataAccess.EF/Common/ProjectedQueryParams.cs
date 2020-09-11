using System;
using System.Linq.Expressions;

namespace BetterTravel.DataAccess.EF.Common
{
    public sealed class ProjectedQueryParams<TEntity, TProjected>
    {
        public int Skip { get; set; }
        public int Top { get; set; }
        public Expression<Func<TEntity, bool>> WherePredicate { get; set; }
        public Expression<Func<TEntity, TProjected>> Projection { get; set; }
    }
    
    public sealed class ProjectedQueryParams<TEntity>
    {
        public int Skip { get; set; }
        public int Top { get; set; }
        public Expression<Func<TEntity, bool>> WherePredicate { get; set; }
    }
}