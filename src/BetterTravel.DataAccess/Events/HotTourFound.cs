using System;
using BetterTravel.DataAccess.Entities;
using BetterTravel.DataAccess.Events.Base;

namespace BetterTravel.DataAccess.Events
{
    public class HotTourFound : IDomainEvent
    {
        public HotTourFound(HotTour hotTour) => 
            HotTour = hotTour ?? throw new ArgumentNullException(nameof(hotTour));

        public HotTour HotTour { get; }
    }
}