using System;
using System.Linq.Expressions;

namespace BetterTravel.DataAccess.Abstraction.Specification
{
    public interface ISpecification<TEntity, TParam>
    {
        bool IsSatisfiedBy(TEntity entity, TParam request);

        Expression<Func<TEntity, bool>> ToExpression(TParam request);
    }
}