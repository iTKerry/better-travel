using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Abstraction.Entities;

namespace BetterTravel.DataAccess.Abstraction.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>> wherePredicate);

        Task<List<TResult>> GetAllAsync<TResult>(
            Expression<Func<T, bool>> wherePredicate,
            Expression<Func<T, TResult>> projection);
    }
}