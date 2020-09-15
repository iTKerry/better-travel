using System;
using BetterExtensions.Domain.Events;
using BetterTravel.DataAccess.Abstractions.Entities;

namespace BetterTravel.DataAccess.Abstractions.Events
{
    public class HotTourFound : IDomainEvent
    {
        public HotTourFound(HotTour hotTour) => 
            HotTour = hotTour ?? throw new ArgumentNullException(nameof(hotTour));

        public HotTour HotTour { get; }
    }
}