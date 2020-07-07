using System;
using System.Linq;
using System.Linq.Expressions;

namespace BetterTravel.Common.Specification
{
    internal sealed class AndSpecification<TEntity, TParam> : Specification<TEntity, TParam>
    {
        private readonly Specification<TEntity, TParam> _left; 
        private readonly Specification<TEntity, TParam> _right;
        
        public AndSpecification(
            Specification<TEntity, TParam> left, 
            Specification<TEntity, TParam> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<TEntity, bool>> ToExpression(TParam request)
        {
            var leftExpr = _left.ToExpression(request);
            var rightExpr = _right.ToExpression(request);

            var andExpr = Expression.AndAlso(leftExpr, rightExpr);

            return Expression.Lambda<Func<TEntity, bool>>(andExpr, leftExpr.Parameters.Single());
        }
    }
}