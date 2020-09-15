using CSharpFunctionalExtensions;

namespace BetterTravel.DataAccess.Abstractions.ValueObjects
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

        protected override bool EqualsCore(Resort other) => 
            Name == other.Name;

        protected override int GetHashCodeCore() => 
            Name.GetHashCode();
    }
}