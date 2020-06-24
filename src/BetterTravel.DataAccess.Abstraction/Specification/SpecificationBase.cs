using System;
using System.Linq.Expressions;

namespace BetterTravel.DataAccess.Abstraction.Specification
{
    public abstract class SpecificationBase<TEntity, TParam> : ISpecification<TEntity, TParam>
    {
        public abstract Expression<Func<TEntity, bool>> ToExpression(TParam request);
        
        public bool IsSatisfiedBy(TEntity entity, TParam request)
        {
            var predicate = ToExpression(request).Compile(true);
            return predicate.Invoke(entity);
        }
    }
}