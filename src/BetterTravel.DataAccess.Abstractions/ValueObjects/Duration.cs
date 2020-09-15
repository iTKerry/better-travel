using BetterTravel.DataAccess.Abstractions.Enums;
using CSharpFunctionalExtensions;

namespace BetterTravel.DataAccess.Abstractions.ValueObjects
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

        protected override bool EqualsCore(Duration other) => 
            Count == other.Count && Type == other.Type;

        protected override int GetHashCodeCore() => 
            Count.GetHashCode() + Type.GetHashCode();
    }
}