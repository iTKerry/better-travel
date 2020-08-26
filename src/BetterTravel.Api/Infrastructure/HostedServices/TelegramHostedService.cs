using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Common.Configurations;
using Microsoft.Extensions.Hosting;
using Serilog;
using Telegram.Bot;

namespace BetterTravel.Api.Infrastructure.HostedServices
{
    public class TelegramHostedService : IHostedService
    {
        private readonly ITelegramBotClient _client;
        private readonly BotConfiguration _configuration;
        private readonly ILogger _logger;

        public TelegramHostedService(
            ITelegramBotClient client,
            BotConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
            _logger = Log.ForContext<TelegramHostedService>();
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _client.DeleteWebhookAsync(cancellationToken);
            _logger.Information($"Setting web-hook with {_configuration.WebhookUrl} {_configuration.BotToken}");
            
            await _client.SetWebhookAsync($"{_configuration.WebhookUrl}/{_configuration.BotToken}",
                cancellationToken: cancellationToken);
            _logger.Information("End setting web-hook");
        }

        public async Task StopAsync(CancellationToken cancellationToken) => 
            await _client.DeleteWebhookAsync(cancellationToken);
    }
}