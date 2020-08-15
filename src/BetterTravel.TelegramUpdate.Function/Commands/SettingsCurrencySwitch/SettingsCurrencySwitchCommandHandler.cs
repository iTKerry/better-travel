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

namespace BetterTravel.TelegramUpdate.Function.Commands.SettingsCurrencySwitch
{
    public class SettingsCurrencySwitchCommandHandler : TelegramCommandHandler<SettingsCurrencySwitchCommand>
    {
        public SettingsCurrencySwitchCommandHandler(
            IUnitOfWork unitOfWork, ITelegramBotClient client) 
            : base(unitOfWork, client) { }

        public override async Task<IHandlerResult> Handle(SettingsCurrencySwitchCommand request, CancellationToken ctx) =>
            await UnitOfWork.ChatRepository
                .GetFirstAsync(c => c.ChatId == request.ChatId)
                .ToResult("That chat wasn't found between our subscribers.")
                .Tap(chat => chat.ChangeCurrency(request.Currency))
                .Tap(chat => UnitOfWork.ChatRepository.Save(chat))
                .Tap(() => UnitOfWork.CommitAsync())
                .Bind(GetKeyboardDataResult)
                .Bind(GetMarkupResult)
                .Tap(markup => EditReplyMarkupAsync(request.ChatId, request.MessageId, markup, ctx))
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

        private async Task<Message> EditReplyMarkupAsync(
            long chatId, int messageId, InlineKeyboardMarkup markup, CancellationToken token) => 
            await Client.EditMessageReplyMarkupAsync(chatId, messageId, markup, token);
    }
}