using System;

namespace BetterTravel.Common.Configurations
{
    public class DbConnectionString
    {
        public DbConnectionString(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Invalid connection string!", nameof(value));
            
            Value = value;
        }

        public string Value { get; }
    }
}