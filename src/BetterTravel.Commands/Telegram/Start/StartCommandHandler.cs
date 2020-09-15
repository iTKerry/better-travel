using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Commands.Abstractions;
using BetterTravel.DataAccess.Abstractions.Entities;
using BetterTravel.DataAccess.Abstractions.Repository;
using BetterTravel.DataAccess.Abstractions.ValueObjects;
using BetterTravel.MediatR.Core.Abstractions;
using CSharpFunctionalExtensions;
using Telegram.Bot;
using Chat = BetterTravel.DataAccess.Abstractions.Entities.Chat;

namespace BetterTravel.Commands.Telegram.Start
{
    public class StartCommandHandler : TelegramCommandHandler<StartCommand>
    {
        public StartCommandHandler(
            IUnitOfWork unitOfWork, ITelegramBotClient telegram)
            : base(unitOfWork, telegram) { }

        public override async Task<IHandlerResult> Handle(StartCommand request, CancellationToken ctx)
        {
            if (request.IsBot)
                return ValidationFailed("Bots are not allowed to subscribe.");

            Maybe<Chat> maybeChat = await UnitOfWork.ChatWriteRepository
                .GetFirstAsync(c => c.ChatId == request.ChatId);
            
            return await maybeChat.ToResult("Chat was not found.")
                .Tap(chat => SendMessageAsync(request.ChatId, "You were subscribed previously.", ctx))
                .Tap(chat => chat.UpdateInfo(request.Title, request.Description, request.Type))
                .OnFailure(() => SendMessageAsync(request.ChatId, "Welcome message.", ctx))
                .OnFailureCompensate(() => CreateChat(request)
                    .Tap(chat => UnitOfWork.ChatWriteRepository.Save(chat))
                    .Tap(() => SendMessageAsync(request.ChatId, "You are now subscribed to updates.", ctx)))
                .Finally(result => result.IsFailure
                    ? ValidationFailed(result.Error)
                    : Ok());
        }

        private static Result<Chat> CreateChat(StartCommand request)
        {
            var infoResult = ChatInfo.Create(request.Title, request.Description, request.Type);
            var settingsResult = ChatSettings.Create();

            return Result
                .Combine(infoResult, settingsResult)
                .Bind(() => Chat.Create(request.ChatId, infoResult.Value, settingsResult.Value));
        }
    }
}