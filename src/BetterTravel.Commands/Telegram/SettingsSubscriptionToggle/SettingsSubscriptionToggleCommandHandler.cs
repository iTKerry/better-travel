using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Commands.Abstractions;
using BetterTravel.Commands.Telegram.Settings.Keyboard;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.MediatR.Core.Abstractions;
using CSharpFunctionalExtensions;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Chat = BetterTravel.DataAccess.Entities.Chat;

namespace BetterTravel.Commands.Telegram.SettingsSubscriptionToggle
{
    public class SettingsSubscriptionToggleCommandHandler : TelegramCommandHandler<SettingsSubscriptionToggleCommand>
    {
        public SettingsSubscriptionToggleCommandHandler(
            IUnitOfWork unitOfWork, ITelegramBotClient telegram)
            : base(unitOfWork, telegram) { }

        public override async Task<IHandlerResult> Handle(
            SettingsSubscriptionToggleCommand request, 
            CancellationToken ctx) =>
            await UnitOfWork.ChatRepository
                .GetFirstAsync(c => c.ChatId == request.ChatId)
                .ToResult("Chat was not found.")
                .Tap(chat => chat.ToggleSubscription())
                .Tap(chat => UnitOfWork.ChatRepository.Save(chat))
                .Bind(GetKeyboardDataResult)
                .Bind(GetMarkupResult)
                .Tap(markup => EditMessageReplyMarkupAsync(request.ChatId, request.MessageId, markup, ctx))
                .Finally(result => result.IsFailure 
                    ? ValidationFailed(result.Error) 
                    : Ok());

        private static Result<SettingsKeyboardData> GetKeyboardDataResult(Chat chat) => 
            Result.Success(new SettingsKeyboardData {IsSubscribed = chat.Settings.IsSubscribed});
        
        private static Result<InlineKeyboardMarkup> GetMarkupResult(SettingsKeyboardData data) => 
            Result.Success(new SettingsKeyboard().ConcreteKeyboardMarkup(data));
    }
}