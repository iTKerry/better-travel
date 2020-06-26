using System;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Commands.HotTours.FetchHotTours;
using MediatR;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BetterTravel.Api.Infrastructure.HostedServices
{
    public class HotToursFetcherHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public HotToursFetcherHostedService(IMediator mediator)
        {
            _mediator = mediator;
            _logger = Log.ForContext<HotToursFetcherHostedService>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Information($"{nameof(HotToursFetcherHostedService)} running.");
            _timer = new Timer(DoBackgroundJob, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
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
                var command = new FetchHotToursCommand();
                await _mediator.Send(command);
            }
            catch (Exception e)
            {
                _logger.Error(e, nameof(HotToursFetcherHostedService));
            }
        }
    }
}