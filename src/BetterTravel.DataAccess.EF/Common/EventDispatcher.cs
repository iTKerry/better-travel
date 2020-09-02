using System;
using System.Collections.Generic;
using System.Linq;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.DataAccess.Events;
using BetterTravel.DataAccess.Events.Base;

namespace BetterTravel.DataAccess.EF.Common
{
    public sealed class EventDispatcher : IEventDispatcher
    {
        public void Dispatch(int entityId, IEnumerable<IDomainEvent> domainEvents) =>
            domainEvents.ToList().ForEach(ev => Dispatch(entityId, ev));

        public void Dispatch(int entityId, IDomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case HotTourFound hotTourFound:
                    //TODO: store to redis-cache here
                    Console.WriteLine(
                        "Type: HOT_TOUR_FOUND; " +
                        $"Id: {entityId}; " +
                        $"Title: {hotTourFound.Title}");
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}