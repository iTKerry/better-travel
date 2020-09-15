using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Commands.Abstractions;
using BetterTravel.Commands.Telegram.Settings.Keyboard;
using BetterTravel.DataAccess.Abstractions.Repository;
using BetterTravel.MediatR.Core.Abstractions;
using CSharpFunctionalExtensions;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Chat = BetterTravel.DataAccess.Abstractions.Entities.Chat;

namespace BetterTravel.Commands.Telegram.Settings
{
    public class SettingsCommandHandler : TelegramCommandHandler<SettingsCommand>
    {
        private const string KeyboardMessage = "Configure your subscription settings here";

        public SettingsCommandHandler(
            IUnitOfWork unitOfWork, ITelegramBotClient telegram)
            : base(unitOfWork, telegram) { }

        public override async Task<IHandlerResult> Handle(
            SettingsCommand request, 
            CancellationToken ctx)
        {
            Maybe<Chat> maybeChat = await UnitOfWork.ChatWriteRepository
                .GetFirstAsync(c => c.ChatId == request.ChatId);
            
            return await maybeChat
                .ToResult("That chat wasn't found between our subscribers.")
                .Bind(GetKeyboardDataResult)
                .Bind(GetMarkupResult)
                .Tap(markup => SendMessageAsync(request.ChatId, KeyboardMessage, markup, ctx))
                .Finally(result => result.IsFailure
                    ? ValidationFailed(result.Error)
                    : Ok());
        }

        private static Result<SettingsKeyboardData> GetKeyboardDataResult(Chat chat) =>
            Result.Success(GetSettingsKeyboardData(chat));

        private static Result<InlineKeyboardMarkup> GetMarkupResult(SettingsKeyboardData data) => 
            Result.Success(new SettingsKeyboard().ConcreteKeyboardMarkup(data));

        private static SettingsKeyboardData GetSettingsKeyboardData(Chat chat) =>
            new SettingsKeyboardData {IsSubscribed = chat.Settings.IsSubscribed};
    }
}