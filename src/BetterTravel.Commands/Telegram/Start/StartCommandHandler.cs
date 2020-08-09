using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Commands.Abstractions;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.Domain.Entities;
using BetterTravel.Domain.ValueObjects;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using CSharpFunctionalExtensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chat = BetterTravel.Domain.Entities.Chat;

namespace BetterTravel.Commands.Telegram.Start
{
    public class StartCommandHandler : CommandHandlerBase<StartCommand>
    {
        private readonly ITelegramBotClient _telegram;

        public StartCommandHandler(IUnitOfWork unitOfWork, ITelegramBotClient telegram) 
            : base(unitOfWork) =>
            _telegram = telegram;

        public override async Task<IHandlerResult> Handle(StartCommand request, CancellationToken ctx)
        {
            if (request.IsBot)
                return ValidationFailed("Bots are not allowed to subscribe.");

            return await
                UnitOfWork.ChatRepository
                    .GetFirstAsync(c => c.ChatId == request.ChatId)
                    .ToResult("Chat was not found.")
                    .Tap(chat => SendMessageAsync(request.ChatId, "You were subscribed previously.", ctx))
                    .Tap(chat => chat.UpdateInfo(request.Title, request.Description, request.Type))
                    .OnFailure(() => SendMessageAsync(request.ChatId, "Welcome message.", ctx))
                    .OnFailureCompensate(() => CreateChat(request)
                        .Tap(chat => chat.ToggleSubscription())
                        .Tap(chat => UnitOfWork.ChatRepository.Save(chat))
                        .Tap(() => UnitOfWork.CommitAsync())
                        .Tap(() => SendMessageAsync(request.ChatId, "You are now subscribed to updates.", ctx)))
                    .Finally(result => result.IsFailure
                        ? ValidationFailed(result.Error)
                        : Ok());
        }

        private static Result<Chat> CreateChat(StartCommand request)
        {
            var infoResult = ChatInfo.Create(request.Title, request.Description, request.Type);
            var settingsResult = ChatSettings.Create(false);

            return Result
                .Combine(infoResult, settingsResult)
                .Bind(() => Chat.Create(request.ChatId, infoResult.Value, settingsResult.Value));
        }

        private async Task<Message> SendMessageAsync(long chatId, string message, CancellationToken token) => 
            await _telegram.SendTextMessageAsync(chatId, message, cancellationToken: token);
    }
}