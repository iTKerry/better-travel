using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace BetterTravel.CurrencyFetcher.Function.Triggers
{
    public  class ExchangeFetcherTimerFunction
    {
        private readonly IExchangeProvider _exchangeProvider;

        public ExchangeFetcherTimerFunction(IExchangeProvider exchangeProvider) => 
            _exchangeProvider = exchangeProvider;

        [FunctionName(nameof(ExchangeFetcherTimerFunction))]
        public async Task RunAsync(
            [TimerTrigger("0 */5 * * * *", RunOnStartup = true)]
            TimerInfo myTimer, ILogger log) =>
            await _exchangeProvider.GetExchangeAsync(false)
                .Map(currencies => currencies
                    .Select(c => $"Code: {c.Type}\t| Rate: {c.Rate:F}")
                    .Aggregate(
                        new StringBuilder(),
                        (left, right) => left.Length != 0
                            ? left.Append("\n").Append(right)
                            : left.Append(right),
                        builder => builder.ToString()))
                .OnFailure(error => log.LogError(error));
    }
}