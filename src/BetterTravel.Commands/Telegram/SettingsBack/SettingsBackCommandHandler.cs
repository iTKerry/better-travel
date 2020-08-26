using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Commands.Abstractions;
using BetterTravel.Commands.Telegram.Keyboards.Data;
using BetterTravel.Commands.Telegram.Keyboards.Factories;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.MediatR.Core.Abstractions;
using CSharpFunctionalExtensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Chat = BetterTravel.DataAccess.Entities.Chat;

namespace BetterTravel.Commands.Telegram.SettingsBack
{
    public class SettingsBackCommandHandler : TelegramCommandHandler<SettingsBackCommand>
    {
        private const string KeyboardMessage = "Configure your subscription settings here";

        public SettingsBackCommandHandler(
            IUnitOfWork unitOfWork, ITelegramBotClient telegram)
            : base(unitOfWork, telegram) { }

        public override async Task<IHandlerResult> Handle(
            SettingsBackCommand request, 
            CancellationToken ctx) =>
            await UnitOfWork.ChatRepository
                .GetFirstAsync(c => c.ChatId == request.ChatId)
                .ToResult("That chat wasn't found between our subscribers.")
                .Bind(GetKeyboardDataResult)
                .Bind(GetMarkupResult)
                .Tap(() => EditMessageTextAsync(request.ChatId, request.MessageId, KeyboardMessage, ctx))
                .Tap(markup => EditMessageReplyMarkupAsync(request.ChatId, request.MessageId, markup, ctx))
                .Finally(result => result.IsFailure 
                    ? ValidationFailed(result.Error) 
                    : Ok());
        
        private static Result<SettingsKeyboardData> GetKeyboardDataResult(Chat chat) => 
            Result.Success(new SettingsKeyboardData {IsSubscribed = chat.Settings.IsSubscribed});
        
        private static Result<InlineKeyboardMarkup> GetMarkupResult(SettingsKeyboardData data) => 
            Result.Success(new SettingsKeyboard().ConcreteKeyboardMarkup(data));

        private async Task<Message> EditMessageTextAsync(
            long chatId, int messageId, string message, CancellationToken token) => 
            await Client.EditMessageTextAsync(chatId, messageId, message, cancellationToken: token);
        
        private async Task<Message> EditMessageReplyMarkupAsync(
            long chatId, int messageId, InlineKeyboardMarkup markup, CancellationToken token) => 
            await Client.EditMessageReplyMarkupAsync(chatId, messageId, markup, token);
    }
}