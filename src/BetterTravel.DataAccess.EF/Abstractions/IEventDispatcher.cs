using System.Collections.Generic;
using BetterTravel.DataAccess.Events.Base;

namespace BetterTravel.DataAccess.EF.Abstractions
{
    public interface IEventDispatcher
    {
        void Dispatch(int entityId, IEnumerable<IDomainEvent> domainEvents);
        void Dispatch(int entityId, IDomainEvent domainEvent);
    }
}