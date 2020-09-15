using CSharpFunctionalExtensions;

namespace BetterTravel.DataAccess.Abstractions.ValueObjects
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

        protected override bool EqualsCore(Name other) =>
            First == other.First && Last == other.Last;

        protected override int GetHashCodeCore() => 
            First.GetHashCode() + Last.GetHashCode();
    }
}