using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.DataAccess.EF.Common;
using BetterTravel.DataAccess.Views.Base;

namespace BetterTravel.DataAccess.EF.Abstractions
{
    public interface IReadOnlyRepository<TView> 
        where TView : View
    {
        Task<List<TView>> GetAllAsync(
            ProjectedQueryParams<TView> projectedProjectedQueryParams,
            CancellationToken cancellationToken = default);
        
        Task<List<TResult>> GetAllAsync<TResult>(
            ProjectedQueryParams<TView, TResult> projectedQueryParams,
            CancellationToken cancellationToken = default);
    }
}