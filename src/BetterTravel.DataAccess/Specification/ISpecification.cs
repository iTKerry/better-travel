using System;
using System.Linq.Expressions;

namespace BetterTravel.DataAccess.Specification
{
    public interface ISpecification<TEntity, TParam>
    {
        bool IsSatisfiedBy(TEntity entity, TParam request);

        Expression<Func<TEntity, bool>> ToExpression(TParam request);
    }
}