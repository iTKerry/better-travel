using System.Threading.Tasks;
using AutoMapper;
using BetterTravel.Commands.Abstractions;
using BetterTravel.Commands.Telegram.Start;
using BetterTravel.Commands.Telegram.Status;
using BetterTravel.Commands.Telegram.Subscribe;
using BetterTravel.Commands.Telegram.Unsubscribe;
using BetterTravel.MediatR.Core.HandlerResults;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BetterTravel.Api.Controllers
{
    [Route("api/[controller]")]
    public class TelegramController : ApiController
    {
        private readonly ITelegramBotClient _client;
        
        public TelegramController(IMapper mapper, IMediator mediator, ITelegramBotClient client) 
            : base(mapper, mediator) =>
            _client = client;

        [HttpPost("update/{token}")]
        public async Task Update([FromRoute] string token, [FromBody] Update update)
        {
            var command = GetCommand(update);
            var result = await Mediator.Send(command);
            await HandleResultAsync(result, update.Message.Chat.Id);
        }

        private ICommand GetCommand(Update update) =>
            CommandWithoutSignature(update) switch
            {
                "/start" => Mapper.Map<StartCommand>(update),
                "/subscribe" => Mapper.Map<SubscribeCommand>(update),
                "/unsubscribe" => Mapper.Map<UnsubscribeCommand>(update),
                "/status" => Mapper.Map<StatusCommand>(update),
                _ => null
            };

        private static string CommandWithoutSignature(Update update) => 
            update.Message.Text
                .Replace("@BetterTravelBot", string.Empty);

        private async Task<Message> HandleResultAsync(IHandlerResult result, long chatId) =>
            result switch
            {
                ValidationFailedHandlerResult vf => await _client.SendTextMessageAsync(chatId, vf.Message),
                _ => null
            };
    }
}