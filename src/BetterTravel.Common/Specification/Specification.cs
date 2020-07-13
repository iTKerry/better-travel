using System;
using System.Linq.Expressions;

namespace BetterTravel.Common.Specification
{
    public abstract class Specification<TEntity, TParam>
    {
        public static readonly Specification<TEntity, TParam> All = new IdentitySpecification<TEntity, TParam>();
        
        public abstract Expression<Func<TEntity, bool>> ToExpression(TParam request);
        
        public bool IsSatisfiedBy(TEntity entity, TParam request)
        {
            var predicate = ToExpression(request).Compile(true);
            return predicate.Invoke(entity);
        }

        public Specification<TEntity, TParam> And(Specification<TEntity, TParam> specification)
        {
            if (this == All)
                return specification;
            if (specification == All)
                return this;
            
            return new AndSpecification<TEntity, TParam>(this, specification);
        }

        public Specification<TEntity, TParam> Or(Specification<TEntity, TParam> specification)
        {
            if (this == All || specification == All)
                return All;
                
            return new OrSpecification<TEntity, TParam>(this, specification);
        }

        public Specification<TEntity, TParam> Not() =>
            new NotSpecification<TEntity, TParam>(this);
    }
}