using System;
using BetterTravel.DataAccess.Abstraction.ValueObjects.Base;

namespace BetterTravel.DataAccess.Abstraction.ValueObjects
{
    public class Departure : ValueObject<Departure>
    {
        protected Departure()
        {
        }
        
        public Departure(string location, DateTime date)
        {
            Location = location;
            Date = date;
        }

        public string Location { get; private set; }
        public DateTime Date { get; private set; }
        
        protected override int GetHashCodeCore() => 
            Location.GetHashCode() + Date.GetHashCode();

        protected override bool EqualCore(Departure other) => 
            Location == other.Location && Date == other.Date;
    }
}