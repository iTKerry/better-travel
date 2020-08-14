using System.Threading;
using System.Threading.Tasks;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.MediatR.Core.Abstractions;
using BetterTravel.TelegramUpdate.Function.Commands.Abstractions;
using BetterTravel.TelegramUpdate.Function.Keyboards.Data;
using BetterTravel.TelegramUpdate.Function.Keyboards.Factories;
using CSharpFunctionalExtensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Chat = BetterTravel.Domain.Entities.Chat;

namespace BetterTravel.TelegramUpdate.Function.Commands.Settings
{
    public class SettingsCommandHandler : TelegramCommandHandler<SettingsCommand>
    {
        private const string KeyboardMessage = "Configure your subscription settings here";

        public SettingsCommandHandler(
            IUnitOfWork unitOfWork, ITelegramBotClient telegram)
            : base(unitOfWork, telegram) { }

        public override async Task<IHandlerResult> Handle(
            SettingsCommand request, 
            CancellationToken ctx) =>
            await UnitOfWork.ChatRepository
                .GetFirstAsync(c => c.ChatId == request.ChatId)
                .ToResult("That chat wasn't found between our subscribers.")
                .Bind(GetKeyboardDataResult)
                .Bind(GetMarkupResult)
                .Tap(markup => SendMessageAsync(request.ChatId, KeyboardMessage, markup, ctx))
                .Finally(result => result.IsFailure
                    ? ValidationFailed(result.Error)
                    : Ok());

        private static Result<SettingsKeyboardData> GetKeyboardDataResult(Chat chat) => 
            Result.Success(new SettingsKeyboardData {IsSubscribed = chat.Settings.IsSubscribed});
        
        private static Result<InlineKeyboardMarkup> GetMarkupResult(SettingsKeyboardData data) => 
            Result.Success(new SettingsKeyboard().ConcreteKeyboardMarkup(data));

        private async Task<Message> SendMessageAsync(
            long chatId, string message, InlineKeyboardMarkup markup, CancellationToken token) => 
            await Client.SendTextMessageAsync(chatId, message, replyMarkup: markup, cancellationToken: token);
    }
}