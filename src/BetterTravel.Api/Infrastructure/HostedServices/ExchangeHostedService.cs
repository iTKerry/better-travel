using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterTravel.Api.Infrastructure.HostedServices.Abstractions;
using BetterTravel.Application.Exchange.Abstractions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BetterTravel.Api.Infrastructure.HostedServices
{
    public class ExchangeHostedService : TimedHostedService<ExchangeHostedService>
    {
        private readonly ILogger<ExchangeHostedService> _logger;
        private readonly IExchangeProvider _exchangeProvider;

        protected override TimeSpan Period => TimeSpan.FromHours(1);

        public ExchangeHostedService(
            ILogger<ExchangeHostedService> logger,
            IExchangeProvider exchangeProvider,
            IServiceProvider services)
            : base(services, logger) =>
            (_logger, _exchangeProvider) = (logger, exchangeProvider);

        protected override async Task JobAsync(IServiceScope serviceScope) =>
            await _exchangeProvider.GetExchangeAsync(false)
                .Map(currencies => currencies
                    .Select(c => $"Code: {c.Type}\t| Rate: {c.Rate:F}")
                    .Aggregate(
                        new StringBuilder(),
                        (left, right) => left.Length != 0
                            ? left.Append("\n").Append(right)
                            : left.Append(right),
                        builder => builder.ToString()))
                .Tap(currencies => _logger.LogInformation($"Fetched exchange:\n{currencies}"))
                .OnFailure(error => _logger.LogError(error));
    }
}