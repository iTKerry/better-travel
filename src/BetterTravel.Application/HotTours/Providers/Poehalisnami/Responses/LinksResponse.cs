using System.Collections.Generic;

namespace BetterTravel.Application.HotTours.Providers.Poehalisnami.Responses
{
    public class LinksResponse
    {
        public bool OpenInNewWindow { get; set; }
        public List<LinkResponse> Links { get; set; }
        public string ListDelimiter { get; set; }
    }
}