using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterExtensions.AspNet.HostedServices;
using BetterTravel.Application.Exchange.Abstractions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BetterTravel.Api.Infrastructure.HostedServices
{
    public class ExchangeHostedService : TimedHostedService<ExchangeHostedService>
    {
        private readonly IExchangeProvider _exchangeProvider;

        protected override TimeSpan Period => TimeSpan.FromHours(1);

        public ExchangeHostedService(
            ILogger<ExchangeHostedService> logger,
            IExchangeProvider exchangeProvider,
            IServiceScopeFactory scopeFactory)
            : base(logger, scopeFactory) =>
            _exchangeProvider = exchangeProvider;

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