using BetterTravel.DataAccess.Enums;
using BetterTravel.DataAccess.ValueObjects.Base;

namespace BetterTravel.DataAccess.ValueObjects
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

        public int Count { get; }
        public DurationType Type { get; }
        
        protected override int GetHashCodeCore() => 
            Count.GetHashCode() + Type.GetHashCode();

        protected override bool EqualCore(Duration other) => 
            Count == other.Count && Type == other.Type;
    }
}