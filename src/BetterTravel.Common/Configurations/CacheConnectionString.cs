using System;

namespace BetterTravel.Common.Configurations
{
    public class CacheConnectionString
    {
        public CacheConnectionString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Invalid connection string!", nameof(value));
            
            Value = value;
        }

        public string Value { get; set; }

        public override string ToString() => Value;
    }
}