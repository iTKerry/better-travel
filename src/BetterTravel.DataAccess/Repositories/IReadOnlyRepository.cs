using System.Collections.Generic;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Views;
using BetterTravel.DataAccess.Views.Base;

namespace BetterTravel.DataAccess.Repositories
{
    public interface IReadOnlyRepository<TView> 
        where TView : View
    {
        Task<List<TView>> GetAsync(QueryObject<TView> queryObject);
        Task<List<TResult>> GetAsync<TResult>(QueryObject<TView, TResult> queryObject);
    }
}