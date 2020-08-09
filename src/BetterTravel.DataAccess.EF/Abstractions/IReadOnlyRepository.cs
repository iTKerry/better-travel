using System.Collections.Generic;
using System.Threading.Tasks;
using BetterTravel.DataAccess.EF.Repositories;
using BetterTravel.Domain.Views.Base;

namespace BetterTravel.DataAccess.EF.Abstractions
{
    public interface IReadOnlyRepository<TView> 
        where TView : View
    {
        Task<List<TView>> GetAsync(QueryObject<TView> queryObject);
        Task<List<TResult>> GetAsync<TResult>(QueryObject<TView, TResult> queryObject);
    }
}