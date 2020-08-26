using System;
using BetterTravel.DataAccess.Events.Base;

namespace BetterTravel.DataAccess.Events
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