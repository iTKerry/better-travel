using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Application.Abstractions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BetterTravel.Api.Infrastructure.HostedServices
{
    public class ExchangeHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<ExchangeHostedService> _logger;
        private readonly IExchangeProvider _exchangeProvider;
        private Timer _timer;

        public ExchangeHostedService(
            ILogger<ExchangeHostedService> logger, 
            IExchangeProvider exchangeProvider)
        {
            _logger = logger;
            _exchangeProvider = exchangeProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(ExchangeHostedService)} started...");
            _timer = new Timer(GetExchangeAsync, null, TimeSpan.Zero, TimeSpan.FromHours(1));
            return Task.CompletedTask;
        }

        private async void GetExchangeAsync(object sender)
        {
            try
            {
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
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(ExchangeHostedService)} stopped...");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose() => 
            _timer?.Dispose();
    }
}