using System.Collections.Generic;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Events.Base;

namespace BetterTravel.DataAccess.EF.Abstractions
{
    public interface IEventDispatcher
    {
        Task DispatchAsync(int entityId, IEnumerable<IDomainEvent> domainEvents);
        Task DispatchAsync(int entityId, IDomainEvent domainEvent);
    }
}