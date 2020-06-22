using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BetterTravel.Api.Infrastructure.HostedServices
{
    public class HotToursHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ILogger _logger;

        public HotToursHostedService(Timer timer)
        {
            _timer = timer;
            _logger = Log.ForContext<HotToursHostedService>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Information($"{nameof(HotToursHostedService)} running.");
            _timer = new Timer(DoBackgroundJob, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.Information($"{nameof(HotToursHostedService)} is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose() => 
            _timer?.Dispose();
        
        private async void DoBackgroundJob(object state)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                _logger.Error($"{nameof(HotToursHostedService)}", e);
            }
        }
    }
}