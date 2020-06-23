using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Commands.Abstractions;
using BetterTravel.DataAccess.Abstraction.Repositories;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using CSharpFunctionalExtensions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BetterTravel.Commands.Telegram.Status
{
    public class StatusCommandHandler : CommandHandlerBase<StatusCommand>
    {
        private readonly ITelegramBotClient _telegram;
        
        public StatusCommandHandler(IUnitOfWork unitOfWork, ITelegramBotClient telegram) 
            : base(unitOfWork)
        {
            _telegram = telegram;
        }

        public override async Task<IHandlerResult> Handle(
            StatusCommand request,
            CancellationToken cancellationToken) =>
            await UnitOfWork.ChatRepository
                .GetByAsync(chat => chat.ChatId == request.ChatId)
                .ToResult("That chat wasn't found between our subscribers.")
                .Tap(chat => chat.IsSubscribed
                    ? SendMessageAsync(chat.ChatId, "You are subscribed", cancellationToken)
                    : SendMessageAsync(chat.ChatId, "You are unsubscribed", cancellationToken))
                .Finally(result => result.IsFailure
                    ? ValidationFailed(result.Error)
                    : Ok());

        private async Task<Message> SendMessageAsync(long chatId, string message, CancellationToken token) => 
            await _telegram.SendTextMessageAsync(chatId, message, cancellationToken: token);
    }
}