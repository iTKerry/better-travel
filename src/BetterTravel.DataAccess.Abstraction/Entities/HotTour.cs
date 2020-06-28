using BetterTravel.DataAccess.Abstraction.Entities.Base;
using BetterTravel.DataAccess.Abstraction.ValueObjects;

namespace BetterTravel.DataAccess.Abstraction.Entities
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
    }
}