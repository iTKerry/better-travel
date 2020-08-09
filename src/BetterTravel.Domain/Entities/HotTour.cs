using BetterTravel.Domain.Entities.Base;
using BetterTravel.Domain.Entities.Enumerations;
using BetterTravel.Domain.Events;
using BetterTravel.Domain.ValueObjects;

namespace BetterTravel.Domain.Entities
{
    public class HotTour : AggregateRoot
    {
        protected HotTour()
        {
        }
        
        public HotTour(
            HotTourInfo info,
            HotelCategory category,
            Duration duration, 
            Price price, 
            Country country, 
            Resort resort,
            DepartureLocation departureLocation)
        {
            Info = info;
            Category = category;
            Duration = duration;
            Price = price;
            Country = country;
            Resort = resort;
            DepartureLocation = departureLocation;
        }

        public virtual HotTourInfo Info { get; }
        public virtual HotelCategory Category { get; }
        public virtual DepartureLocation DepartureLocation { get; }
        public virtual Duration Duration { get; }
        public virtual Price Price { get; }
        public virtual Country Country { get; }
        public virtual Resort Resort { get; }
        
        public void NotifyFound() => 
            RaiseDomainEvent(new HotTourFound(Info.Name));
    }
}