using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BetterExtensions.Domain.Repository;
using BetterTravel.DataAccess.Abstractions.Entities;

namespace BetterTravel.DataAccess.Abstractions.Repository
{
    public interface IHotToursWriteRepository : IWriteRepository<HotTour>
    {
        Task<List<HotTour>> GetAllAsync(
            Expression<Func<HotTour, bool>> predicate,
            CancellationToken cancellationToken = default);
    }
}