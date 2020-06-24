using BetterTravel.DataAccess.Abstraction.Entities.Base;
using BetterTravel.DataAccess.Abstraction.ValueObjects;

namespace BetterTravel.DataAccess.Abstraction.Entities
{
    public class HotTour : Entity
    {
        protected HotTour()
        {
        }
        
        public HotTour(
            HotTourInfo info,
            Duration duration, 
            Departure departure, 
            Price price, 
            Country country, 
            Resort resort)
        {
            Info = info;
            Duration = duration;
            Departure = departure;
            Price = price;
            Country = country;
            Resort = resort;
        }

        public virtual HotTourInfo Info { get;private set; }
        public virtual Duration Duration { get;private set; }
        public virtual Departure Departure { get;private set; }
        public virtual Price Price { get;private set; }
        public virtual Country Country { get;private set; }
        public virtual Resort Resort { get;private set; }
    }
}