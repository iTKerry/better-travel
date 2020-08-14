using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BetterTravel.CurrencyFetcher.Function
{
    public static class CurrencyFetcherFunction
    {
        private const string DateFormatString = "dd.MM.yyyy";
        private const string ServiceUrl = 
            "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";

        [FunctionName("CurrencyFetcherFunction")]
        public static async Task RunAsync(
            [TimerTrigger("0 */5 * * * *", RunOnStartup = true)] 
            TimerInfo myTimer, ILogger log)
        {
            var json = await new HttpClient().GetStringAsync(ServiceUrl);
            var settings = new JsonSerializerSettings {DateFormatString = DateFormatString};
            var currencies = JsonConvert.DeserializeObject<List<Currency>>(json, settings);
            
            var result = currencies?
                .Select(c => $"Code: {c.CurrencyCode}\t| Rate: {c.Rate:F}")
                .Aggregate(
                    new StringBuilder(),
                    (left, right) => left.Length != 0
                        ? left.Append("\n").Append(right)
                        : left.Append(right),
                    builder => builder.ToString());
        
            log.LogInformation(result);
        }
    }
}