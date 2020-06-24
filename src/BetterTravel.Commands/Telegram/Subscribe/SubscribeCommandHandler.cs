using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Commands.Abstractions;
using BetterTravel.DataAccess.Abstraction.Repositories;
using BetterTravel.DataAccess.Abstraction.ValueObjects;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using CSharpFunctionalExtensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chat = BetterTravel.DataAccess.Abstraction.Entities.Chat;

namespace BetterTravel.Commands.Telegram.Subscribe
{
    public class SubscribeCommandHandler : CommandHandlerBase<SubscribeCommand>
    {
        private readonly ITelegramBotClient _telegram;

        public SubscribeCommandHandler(IUnitOfWork unitOfWork, ITelegramBotClient telegram) 
            : base(unitOfWork) =>
            _telegram = telegram;

        public override async Task<IHandlerResult> Handle(
            SubscribeCommand request, 
            CancellationToken cancellationToken) =>
            await Result
                .SuccessIf(!request.IsBot, "Bots are not allowed to subscribe.")
                .Bind(() => UnitOfWork.ChatRepository
                    .GetFirstAsync(c => c.ChatId == request.ChatId)
                    .ToResult("Chat was not found."))
                .Tap(chat => chat.UpdateInfo(request.Title, request.Description, request.Type))
                .OnFailureCompensate(() => CreateChat(request))
                .Tap(chat => UnitOfWork.ChatRepository.Save(chat))
                .Tap(chat => chat.Subscribe())
                .Tap(() => UnitOfWork.CommitAsync())
                .Tap(() => SendMessageAsync(request.ChatId, "You are now subscribed to updates.", cancellationToken))
                .Finally(result => result.IsFailure 
                    ? ValidationFailed(result.Error) 
                    : Ok());

        private static Result<Chat> CreateChat(SubscribeCommand cmd) =>
            ChatInfo
                .Create(cmd.Title, cmd.Description, cmd.Type)
                .Bind(info => Chat.Create(cmd.ChatId, info, false));
        
        private async Task<Message> SendMessageAsync(long chatId, string message, CancellationToken token) => 
            await _telegram.SendTextMessageAsync(chatId, message, cancellationToken: token);
    }
}