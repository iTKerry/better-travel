using System;

namespace BetterTravel.Domain.Views.Base
{
    public abstract class View
    {
        protected View()
        {
        }
        
        public override bool Equals(object obj)
        {
            if (!(obj is View other))
                return false;

            if (ReferenceEquals(other, this))
                return true;

            if (GetRealType() != other.GetRealType())
                return false;

            return false;
        }

        public static bool operator ==(View a, View b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(View a, View b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return GetType().ToString().GetHashCode();
        }

        private Type GetRealType()
        {
            var type = GetType();
            return type.ToString().Contains("Castle.Proxies.") 
                ? type.BaseType 
                : type;
        }
    }
}