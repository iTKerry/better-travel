using System;
using Newtonsoft.Json;

namespace BetterTravel.Application.Exchange
{
    public class ExchangeResponse
    {
        [JsonProperty("r030")]
        public int CurrencyNumber { get; set; }
        
        [JsonProperty("txt")]
        public string CurrencyName { get; set; }
        
        [JsonProperty("rate")]
        public double Rate { get; set; }
        
        [JsonProperty("cc")]
        public string CurrencyCode { get; set; }
        
        [JsonProperty("exchangedate")]
        public DateTime ExchangeDate { get; set; }
    }
}