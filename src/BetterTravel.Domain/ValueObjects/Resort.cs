using BetterTravel.Domain.ValueObjects.Base;

namespace BetterTravel.Domain.ValueObjects
{
    public class Resort : ValueObject<Resort>
    {
        protected Resort()
        {
        }
        
        public Resort(string name)
        {
            Name = name;
        }
        
        public string Name { get; }
        
        protected override int GetHashCodeCore() => 
            Name.GetHashCode();

        protected override bool EqualCore(Resort other) => 
            Name == other.Name;
    }
}