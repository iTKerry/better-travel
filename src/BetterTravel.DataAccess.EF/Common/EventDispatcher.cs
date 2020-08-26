using System;
using System.Collections.Generic;
using System.Linq;
using BetterTravel.DataAccess.Events;
using BetterTravel.DataAccess.Events.Base;

namespace BetterTravel.DataAccess.EF.Common
{
    public interface IEventDispatcher
    {
        void Dispatch(int entityId, IEnumerable<IDomainEvent> domainEvents);
        void Dispatch(int entityId, IDomainEvent domainEvent);
    }

    public sealed class EventDispatcher : IEventDispatcher
    {
        private readonly MessageBus _messageBus;

        public EventDispatcher(MessageBus messageBus) => 
            _messageBus = messageBus;

        public void Dispatch(int entityId, IEnumerable<IDomainEvent> domainEvents) =>
            domainEvents.ToList().ForEach(ev => Dispatch(entityId, ev));

        public void Dispatch(int entityId, IDomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case HotTourFound hotTourFound:
                    _messageBus.SendHotTourFound(entityId, hotTourFound.Title);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
    
    public interface IBus
    {
        void Send(string message);
    }

    public sealed class Bus : IBus
    {
        public void Send(string message)
        {
            Console.WriteLine($"Message sent: '{message}'");
        }
    }

    public sealed class MessageBus
    {
        private readonly IBus _bus;

        public MessageBus(IBus bus) => 
            _bus = bus;

        public void SendHotTourFound(int hotTourId, string title) =>
            _bus.Send("Type: HOT_TOUR_FOUND; " +
                      $"Id: {hotTourId}; " +
                      $"Title: {title}");
    }
}