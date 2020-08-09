using BetterTravel.Domain.ValueObjects.Base;

namespace BetterTravel.Domain.ValueObjects
{
    public class Name : ValueObject<Name>
    {
        protected Name()
        {
        }

        public Name(string first, string last)
        {
            First = first;
            Last = last;
        }

        public string First { get; }
        public string Last { get; }
        
        protected override int GetHashCodeCore() => 
            First.GetHashCode() + Last.GetHashCode();

        protected override bool EqualCore(Name other) =>
            First == other.First && Last == other.Last;
    }
}