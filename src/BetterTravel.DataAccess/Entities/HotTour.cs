using BetterTravel.DataAccess.Entities.Base;
using BetterTravel.DataAccess.Entities.Enumerations;
using BetterTravel.DataAccess.Events;
using BetterTravel.DataAccess.ValueObjects;

namespace BetterTravel.DataAccess.Entities
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