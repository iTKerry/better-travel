using System.Collections.Generic;

namespace BetterTravel.HotToursFetcher.Function.Responses
{
    public class LinksResponse
    {
        public bool OpenInNewWindow { get; set; }
        public List<LinkResponse> Links { get; set; }
        public string ListDelimiter { get; set; }
    }
}