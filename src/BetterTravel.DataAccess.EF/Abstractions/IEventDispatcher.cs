using System.Collections.Generic;
using System.Threading.Tasks;
using BetterExtensions.Domain.Events;

namespace BetterTravel.DataAccess.EF.Abstractions
{
    public interface IEventDispatcher
    {
        Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents);
        Task DispatchAsync(IDomainEvent domainEvent);
    }
}