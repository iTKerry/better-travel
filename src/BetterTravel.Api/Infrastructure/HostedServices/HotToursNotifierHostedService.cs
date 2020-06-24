using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BetterTravel.Api.Infrastructure.HostedServices
{
    public class HotToursNotifierHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ILogger _logger;

        public HotToursNotifierHostedService(Timer timer)
        {
            _timer = timer;
            _logger = Log.ForContext<HotToursFetcherHostedService>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var tickTime = GetTickTime();
            _logger.Information($"{nameof(HotToursNotifierHostedService)} running.");
            _timer = new Timer(DoBackgroundJob, null, TimeSpan.Zero, tickTime);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.Information($"{nameof(HotToursFetcherHostedService)} is stopping.");
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
                _logger.Error($"{nameof(HotToursFetcherHostedService)}", e);
            }
        }
        
        private static TimeSpan GetTickTime()
        {
            var now = DateTime.Now;
            var targetTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute + 1, 0, 0);
            
            if (now > targetTime)
                targetTime = targetTime.AddDays(1);

            var tickTime = targetTime - now;
            return tickTime;
        }
    }
}