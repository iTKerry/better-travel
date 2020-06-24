using System;
using System.Linq;
using BetterTravel.DataAccess.Abstraction.Entities.Base;

namespace BetterTravel.DataAccess.Abstraction.Entities
{
    public class DepartureLocation : Entity
    {
        public static DepartureLocation NoDeparture = new DepartureLocation(0, "No Departure"); 
        public static DepartureLocation Kyiv = new DepartureLocation(1, nameof(Kyiv)); 
        public static DepartureLocation Zaporizhzhia = new DepartureLocation(4, nameof(Zaporizhzhia)); 
        public static DepartureLocation Lviv = new DepartureLocation(6, nameof(Lviv)); 
        public static DepartureLocation Odessa = new DepartureLocation(8, nameof(Odessa)); 
        public static DepartureLocation Kharkiv = new DepartureLocation(11, nameof(Kharkiv));

        public static readonly DepartureLocation[] AllDepartureLocations =
        {
            NoDeparture, Kyiv, Zaporizhzhia, Lviv, Odessa, Kharkiv
        };
        
        protected DepartureLocation()
        {
        }
        
        private DepartureLocation(int id, string name)
            : base(id)
        {
            Name = name;
        }
        
        public string Name { get; }

        public static DepartureLocation FromId(int id) =>
            AllDepartureLocations.SingleOrDefault(country => country.Id == id);

        public static DepartureLocation FromName(string name) =>
            AllDepartureLocations.SingleOrDefault(country =>
                string.Equals(country.Name, name, StringComparison.InvariantCultureIgnoreCase));
    }
}