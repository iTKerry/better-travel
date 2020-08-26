using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BetterTravel.Api.Infrastructure.HostedServices.Abstractions
{
    public abstract class TimedHostedService<T> 
        : IHostedService, IDisposable where T : class
    {
        protected Timer Timer;
        protected ILogger<T> Logger;
        
        protected virtual TimeSpan DueTime => TimeSpan.Zero;
        protected abstract TimeSpan Period { get; }

        protected TimedHostedService(ILogger<T> logger) => 
            Logger = logger;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Timer = new Timer(SafeJobAsync, null, DueTime, Period);
            return Task.CompletedTask;
        }

        private async void SafeJobAsync(object sender)
        {
            try
            {
                await JobAsync();
            }
            catch (Exception e)
            {
                Logger.LogCritical(e, e.Message);
            }
        }

        protected abstract Task JobAsync();

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose() => 
            Timer?.Dispose();
    }
}