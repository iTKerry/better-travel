using System;
using System.Linq;
using System.Linq.Expressions;

namespace BetterTravel.Common.Specification
{
    internal sealed class NotSpecification<TEntity, TParam> : Specification<TEntity, TParam>
    {
        private readonly Specification<TEntity, TParam> _specification; 

        public NotSpecification(Specification<TEntity, TParam> specification) =>
            _specification = specification;

        public override Expression<Func<TEntity, bool>> ToExpression(TParam request)
        {
            var expr = _specification.ToExpression(request);
            
            var notExpr = Expression.Not(expr.Body);

            return Expression.Lambda<Func<TEntity, bool>>(notExpr, expr.Parameters.Single());
        }
    }
}