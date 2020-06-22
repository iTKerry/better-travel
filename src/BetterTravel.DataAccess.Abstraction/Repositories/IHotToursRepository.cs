using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Abstraction.Entities;

namespace BetterTravel.DataAccess.Abstraction.Repositories
{
    public interface IHotToursRepository
    {
        Task<List<HotTour>> GetAllAsync(
            Expression<Func<HotTour, bool>> wherePredicate);

        Task<List<TResult>> GetAllAsync<TResult>(
            Expression<Func<HotTour, bool>> wherePredicate,
            Expression<Func<HotTour, TResult>> projection);
    }
}