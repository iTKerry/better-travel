using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.MediatR.Core;
using BetterTravel.MediatR.Core.Abstractions;
using Telegram.Bot;

namespace BetterTravel.TelegramUpdate.Function.Commands.Abstractions
{
    public abstract class TelegramCommandHandler<TRequest> : CommandHandlerBase<TRequest>
        where TRequest : ICommand
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly ITelegramBotClient Client;

        protected TelegramCommandHandler(
            IUnitOfWork unitOfWork, ITelegramBotClient client) => 
            (UnitOfWork, Client) = 
            (unitOfWork, client);
    }
}