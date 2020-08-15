using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.Domain.Entities;
using BetterTravel.MediatR.Core.Abstractions;
using BetterTravel.TelegramUpdate.Function.Commands.Abstractions;
using BetterTravel.TelegramUpdate.Function.Keyboards.Data;
using BetterTravel.TelegramUpdate.Function.Keyboards.Factories;
using CSharpFunctionalExtensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Chat = BetterTravel.Domain.Entities.Chat;

namespace BetterTravel.TelegramUpdate.Function.Commands.SettingsCurrency
{
    public class SettingsCurrencyCommandHandler : TelegramCommandHandler<SettingsCurrencyCommand>
    {
        private const string KeyboardMessage = "Select preferred currency for you:";

        public SettingsCurrencyCommandHandler(
            IUnitOfWork unitOfWork, ITelegramBotClient client) 
            : base(unitOfWork, client) { }
        
        public override async Task<IHandlerResult> Handle(SettingsCurrencyCommand request, CancellationToken ctx) =>
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

        private static Result<List<SettingsCurrencyKeyboardData>> GetKeyboardDataResult(Chat chat) =>
            Result.Success(Currency.AllCurrencies
                .Where(currency => currency != Currency.Unknown)
                .Select(currency => new SettingsCurrencyKeyboardData
                {
                    Id = currency.Id,
                    Name = currency.Name,
                    IsSubscribed = chat.Settings.Currency == currency
                }).ToList());

        private static Result<InlineKeyboardMarkup> GetMarkupResult(List<SettingsCurrencyKeyboardData> data) => 
            Result.Success(new SettingsCurrencyKeyboard().ConcreteKeyboardMarkup(data));

        private async Task<Message> EditMessageTextAsync(
            long chatId, int messageId, string message, CancellationToken token) => 
            await Client.EditMessageTextAsync(chatId, messageId, message, cancellationToken: token);
        
        private async Task<Message> EditMessageReplyMarkupAsync(
            long chatId, int messageId, InlineKeyboardMarkup markup, CancellationToken token) => 
            await Client.EditMessageReplyMarkupAsync(chatId, messageId, markup, token);
    }
}