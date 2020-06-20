using System.Threading.Tasks;
using AutoMapper;
using BetterTravel.Commands.Abstractions;
using BetterTravel.Commands.Telegram.Start;
using BetterTravel.Commands.Telegram.Status;
using BetterTravel.Commands.Telegram.Subscribe;
using BetterTravel.Commands.Telegram.Unsubscribe;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace BetterTravel.Api.Controllers
{
    public class TelegramController : ApiController
    {
        public TelegramController(IMapper mapper, IMediator mediator) 
            : base(mapper, mediator)
        {
        }
        
        [HttpPost("update/{token}")]
        public async Task Update([FromRoute] string token, [FromBody] Update update)
        {
            var command = GetCommand(update);
            await Mediator.Send(command);
        }

        private ICommand GetCommand(Update update) =>
            update.Message.Text switch
            {
                "/start" => Mapper.Map<StartCommand>(update),
                "/subscribe" => Mapper.Map<SubscribeCommand>(update),
                "/unsubscribe" => Mapper.Map<UnsubscribeCommand>(update),
                "/status" => Mapper.Map<StatusCommand>(update),
                _ => null
            };
    }
}