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
            Duration duration, 
            Price price, 
            Country country, 
            Resort resort)
        {
            Info = info;
            Duration = duration;
            Price = price;
            Country = country;
            Resort = resort;
        }

        public virtual HotTourInfo Info { get; }
        public virtual Duration Duration { get; }
        public virtual Price Price { get; }
        public virtual Country Country { get; }
        public virtual Resort Resort { get; }
    }
}