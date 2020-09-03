using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Cache;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.DataAccess.Events;
using BetterTravel.DataAccess.Events.Base;
using BetterTravel.DataAccess.Redis.Abstractions;
using BetterTravel.DataAccess.Redis.Repositories;

namespace BetterTravel.DataAccess.EF.Common
{
    public sealed class EventDispatcher : IEventDispatcher
    {
        private readonly IHotTourFoundRepository _hotToursCacheRepository;

        public EventDispatcher(IHotTourFoundRepository cacheRepository) =>
            _hotToursCacheRepository = cacheRepository;

        public async Task DispatchAsync(int entityId, IEnumerable<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents) 
                await DispatchAsync(entityId, domainEvent);
        }

        public async Task DispatchAsync(int entityId, IDomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case HotTourFound hotTourFound:
                    var hotTourFoundData = new HotTourFoundData {EntityId = entityId, Title = hotTourFound.Title};
                    await _hotToursCacheRepository.AddValueAsync(hotTourFoundData);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}