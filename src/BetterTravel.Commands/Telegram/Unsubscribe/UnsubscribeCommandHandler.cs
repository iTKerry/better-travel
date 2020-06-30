using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Commands.Abstractions;
using BetterTravel.DataAccess.Repositories;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using CSharpFunctionalExtensions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BetterTravel.Commands.Telegram.Unsubscribe
{
    public class UnsubscribeCommandHandler : CommandHandlerBase<UnsubscribeCommand>
    {
        private readonly ITelegramBotClient _telegram;

        public UnsubscribeCommandHandler(IUnitOfWork unitOfWork, ITelegramBotClient telegram) 
            : base(unitOfWork) =>
            _telegram = telegram;

        public override async Task<IHandlerResult> Handle(
            UnsubscribeCommand request, 
            CancellationToken cancellationToken) =>
            await UnitOfWork.ChatRepository
                .GetFirstAsync(c => c.ChatId == request.ChatId)
                .ToResult("That chat wasn't found between our subscribers.")
                .Tap(chat => UnitOfWork.ChatRepository.Save(chat))
                .Tap(chat => chat.Unsubscribe())
                .Tap(() => UnitOfWork.CommitAsync())
                .Tap(() => SendMessageAsync(request.ChatId, "You are now unsubscribed to updates.", cancellationToken))
                .Finally(result => result.IsFailure
                    ? ValidationFailed(result.Error) 
                    : Ok());

        private async Task<Message> SendMessageAsync(long chatId, string message, CancellationToken token) => 
            await _telegram.SendTextMessageAsync(chatId, message, cancellationToken: token);
    }
}