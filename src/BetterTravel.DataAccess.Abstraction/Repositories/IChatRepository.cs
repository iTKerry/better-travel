using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Abstraction.Entities;

namespace BetterTravel.DataAccess.Abstraction.Repositories
{
    public interface IChatRepository
    {
        Task<List<Chat>> GetAllAsync(
            Expression<Func<Chat, bool>> wherePredicate);

        Task<List<TResult>> GetAllAsync<TResult>(
            Expression<Func<Chat, bool>> wherePredicate,
            Expression<Func<Chat, TResult>> projection);
    }
}