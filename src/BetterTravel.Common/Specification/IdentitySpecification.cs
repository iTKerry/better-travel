using System;
using System.Linq.Expressions;

namespace BetterTravel.Common.Specification
{
    public sealed class IdentitySpecification<TEntity, TParam> : Specification<TEntity, TParam>
    {
        public override Expression<Func<TEntity, bool>> ToExpression(TParam request) => 
            x => true;
    }
}