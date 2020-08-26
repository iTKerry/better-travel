using System;

namespace BetterTravel.Common.Configurations
{
    public class HotToursProviderUri
    {
        public HotToursProviderUri(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Hot tours provider url is empty!", nameof(url));
            
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                throw new ArgumentException($"Invalid hot tours provider url: {url}");
            
            Value = new Uri(url);
        }

        public Uri Value { get; set; }

        public override string ToString() => Value.ToString();
    }
}