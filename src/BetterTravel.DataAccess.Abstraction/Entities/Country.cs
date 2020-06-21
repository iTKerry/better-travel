using System;

namespace BetterTravel.DataAccess.Abstraction.Entities
{
    public class Country
    {
        protected Country()
        {
        }

        public Country(string name, Uri details)
            : base()
        {
            Name = name;
            Details = details;
        }
        
        public string Name { get; }
        public Uri Details { get; }
    }
}