using System.Threading;
using System.Threading.Tasks;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.Domain.Entities;
using BetterTravel.Domain.ValueObjects;
using BetterTravel.MediatR.Core.Abstractions;
using BetterTravel.TelegramUpdate.Function.Commands.Abstractions;
using CSharpFunctionalExtensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chat = BetterTravel.Domain.Entities.Chat;

namespace BetterTravel.TelegramUpdate.Function.Commands.Start
{
    public class StartCommandHandler : TelegramCommandHandler<StartCommand>
    {
        public StartCommandHandler(
            IUnitOfWork unitOfWork, ITelegramBotClient telegram)
            : base(unitOfWork, telegram) { }

        public override async Task<IHandlerResult> Handle(StartCommand request, CancellationToken ctx) =>
            request.IsBot
                ? ValidationFailed("Bots are not allowed to subscribe.")
                : await UnitOfWork.ChatRepository
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

        private static Result<Chat> CreateChat(StartCommand request)
        {
            var infoResult = ChatInfo.Create(request.Title, request.Description, request.Type);
            var settingsResult = ChatSettings.Create();

            return Result
                .Combine(infoResult, settingsResult)
                .Bind(() => Chat.Create(request.ChatId, infoResult.Value, settingsResult.Value));
        }

        private async Task<Message> SendMessageAsync(long chatId, string message, CancellationToken token) => 
            await Client.SendTextMessageAsync(chatId, message, cancellationToken: token);
    }
}