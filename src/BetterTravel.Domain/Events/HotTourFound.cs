using System;
using BetterTravel.Domain.Events.Base;

namespace BetterTravel.Domain.Events
{
    public class HotTourFound : IDomainEvent
    {
        public HotTourFound(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is empty.", nameof(title));
            
            Title = title;
        }

        public string Title { get; }
    }
}