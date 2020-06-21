using System;

namespace BetterTravel.DataAccess.Abstraction.Entities
{
    public class Resort
    {
        protected Resort()
        {
        }
        
        public Resort(string name, Uri details)
            : base()
        {
            Name = name;
            Details = details;
        }
        
        public string Name { get; }
        public Uri Details { get; }
    }
}