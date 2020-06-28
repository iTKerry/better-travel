using System;
using System.Linq;
using BetterTravel.DataAccess.Abstraction.Entities.Base;

namespace BetterTravel.DataAccess.Abstraction.Entities
{
    public class Country : Entity
    {
        public static Country NoCountry = new Country(9999, nameof(NoCountry));
        public static Country Egypt = new Country(11, nameof(Egypt));
        public static Country Turkey = new Country(36, nameof(Turkey));
        public static Country Ukraine = new Country(37, nameof(Ukraine));
        public static Country Spain = new Country(51, nameof(Spain));
        public static Country Bulgaria = new Country(54, nameof(Bulgaria));
        public static Country Tunisia = new Country(61, nameof(Tunisia));
        public static Country Dominican = new Country(62, nameof(Dominican));
        public static Country UAE = new Country(70, nameof(UAE));
        public static Country Greece = new Country(84, nameof(Greece));
        public static Country Georgia = new Country(169, nameof(Georgia));
        public static Country Albania = new Country(591, nameof(Albania));

        public static readonly Country[] AllCountries =
        {
            NoCountry, Egypt, Turkey, Ukraine, Spain, Bulgaria, Tunisia, Dominican, UAE, Greece, Georgia, Albania
        };

        protected Country()
        {
        }

        private Country(int id, string name)
            : base(id)
        {
            Name = name;
        }

        public string Name { get; }

        public static Country FromId(int id) =>
            AllCountries.SingleOrDefault(country => country.Id == id) 
            ?? NoCountry;

        public static Country FromName(string name) =>
            AllCountries.SingleOrDefault(country =>
                string.Equals(country.Name, name, StringComparison.InvariantCultureIgnoreCase)) 
            ?? NoCountry;
    }
}