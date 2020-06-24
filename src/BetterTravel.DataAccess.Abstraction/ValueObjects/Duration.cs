using BetterTravel.DataAccess.Abstraction.Entities.Enums;
using BetterTravel.DataAccess.Abstraction.ValueObjects.Base;

namespace BetterTravel.DataAccess.Abstraction.ValueObjects
{
    public class Duration : ValueObject<Duration>
    {
        protected Duration()
        {
        }
        
        public Duration(int count, DurationType type)
        {
            Count = count;
            Type = type;
        }

        public int Count { get; private set; }
        public DurationType Type { get; private set; }
        
        protected override int GetHashCodeCore() => 
            Count.GetHashCode() + Type.GetHashCode();

        protected override bool EqualCore(Duration other) => 
            Count == other.Count && Type == other.Type;
    }
}