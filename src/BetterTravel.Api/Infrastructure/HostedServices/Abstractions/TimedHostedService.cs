using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BetterTravel.Api.Infrastructure.HostedServices.Abstractions
{
    public abstract class TimedHostedService<T> 
        : IHostedService, IDisposable where T : class
    {
        private readonly IServiceProvider _services;
        
        protected Timer Timer;
        protected ILogger<T> Logger;

        protected virtual TimeSpan DueTime => TimeSpan.Zero;
        protected abstract TimeSpan Period { get; }

        protected TimedHostedService(IServiceProvider services, ILogger<T> logger)
        {
            _services = services;
            Logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Timer = new Timer(SafeJobAsync, null, DueTime, Period);
            return Task.CompletedTask;
        }

        private async void SafeJobAsync(object sender)
        {
            try
            {
                using (var scope = _services.CreateScope())
                {
                    await JobAsync(scope);
                }
            }
            catch (Exception e)
            {
                Logger.LogCritical(e, e.Message);
            }
        }

        protected abstract Task JobAsync(IServiceScope serviceScope);

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose() => 
            Timer?.Dispose();
    }
}