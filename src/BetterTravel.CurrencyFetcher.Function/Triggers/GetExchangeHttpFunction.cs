using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace BetterTravel.CurrencyFetcher.Function.Triggers
{
    public class GetExchangeHttpFunction
    {
        private readonly IExchangeProvider _exchangeProvider;

        public GetExchangeHttpFunction(IExchangeProvider exchangeProvider) => 
            _exchangeProvider = exchangeProvider;

        [FunctionName(nameof(GetExchangeHttpFunction))]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "exchange")]
            HttpRequest req, ILogger log) =>
            await _exchangeProvider.GetExchangeAsync(true)
                .Finally(res => res.IsSuccess
                    ? (IActionResult) new OkObjectResult(res.Value)
                    : (IActionResult) new BadRequestObjectResult(res.Error));
    }
}