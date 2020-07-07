using System;
using System.Linq.Expressions;

namespace BetterTravel.Common.Specification
{
    public abstract class Specification<TEntity, TParam>
    {
        public abstract Expression<Func<TEntity, bool>> ToExpression(TParam request);
        
        public bool IsSatisfiedBy(TEntity entity, TParam request)
        {
            var predicate = ToExpression(request).Compile(true);
            return predicate.Invoke(entity);
        }
        
        public Specification<TEntity, TParam> And(Specification<TEntity, TParam> specification) =>
            new AndSpecification<TEntity, TParam>(this, specification);
        
        public Specification<TEntity, TParam> Or(Specification<TEntity, TParam> specification) =>
            new OrSpecification<TEntity, TParam>(this, specification);
        
        public Specification<TEntity, TParam> Not() =>
            new NotSpecification<TEntity, TParam>(this);
    }
}