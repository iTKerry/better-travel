using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BetterExtensions.Domain.Events;
using BetterTravel.DataAccess.Abstractions.Cache;
using BetterTravel.DataAccess.Abstractions.Events;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.DataAccess.Redis.Abstractions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace BetterTravel.DataAccess.EF.Common
{
    public sealed class EventDispatcher : IEventDispatcher
    {
        private readonly IHotTourFoundRepository _hotToursCacheRepository;
        private readonly ILogger<EventDispatcher> _logger;

        public EventDispatcher(IHotTourFoundRepository cacheRepository, ILogger<EventDispatcher> logger)
        {
            _hotToursCacheRepository = cacheRepository;
            _logger = logger;
        }

        public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents) 
                await DispatchAsync(domainEvent);
        }

        public async Task DispatchAsync(IDomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case HotTourFound hotTourFound:
                    await DispatchHotTourFound(hotTourFound);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        private async Task DispatchHotTourFound(HotTourFound hotTourFound) =>
            await Result
                .FailureIf(hotTourFound is null, $"{nameof(hotTourFound)} is null.")
                .Map(() => new HotTourFoundData
                {
                    EntityId = hotTourFound!.HotTour.Id, 
                    Name = hotTourFound.HotTour.Info.Name
                })
                .Tap(dto => _logger.LogDebug($"{nameof(hotTourFound)}: ID {dto.EntityId}; NAME: {dto.Name}"))
                .Bind(dto => _hotToursCacheRepository.AddValueAsync(dto))
                .OnFailure(error => _logger.LogError(error));
    }
}