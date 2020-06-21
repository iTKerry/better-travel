using System;

namespace BetterTravel.DataAccess.Abstraction.ValueObjects
{
    public class Resort : ValueObject<Resort>
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
        
        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }

        protected override bool EqualCore(Resort other)
        {
            throw new NotImplementedException();
        }
    }
}