using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Application.Abstractions;
using BetterTravel.Commands.Abstractions;
using BetterTravel.Commands.Telegram.Keyboards.Data;
using BetterTravel.Commands.Telegram.Keyboards.Factories;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.DataAccess.Entities;
using BetterTravel.DataAccess.Enums;
using BetterTravel.MediatR.Core.Abstractions;
using CSharpFunctionalExtensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Chat = BetterTravel.DataAccess.Entities.Chat;

namespace BetterTravel.Commands.Telegram.SettingsCurrencySwitch
{
    public class SettingsCurrencySwitchCommandHandler : TelegramCommandHandler<SettingsCurrencySwitchCommand>
    {
        private readonly IExchangeProvider _exchange;

        public SettingsCurrencySwitchCommandHandler(
            IUnitOfWork unitOfWork, 
            ITelegramBotClient client, 
            IExchangeProvider exchange) 
            : base(unitOfWork, client) =>
            _exchange = exchange;

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

        private async Task<Result<List<SettingsCurrencyKeyboardData>>> GetKeyboardDataResult(Chat chat) =>
            (await _exchange.GetExchangeAsync())
            .Map(rates => Currency.AllCurrencies
                .Select(currency => new SettingsCurrencyKeyboardData
                {
                    Id = currency.Id,
                    Name = currency.IsType(CurrencyType.UAH)
                        ? currency.Name
                        : rates.First(rate => currency.IsType(rate.Type)).ToString(),
                    IsSubscribed = chat.Settings.Currency == currency
                }).ToList());

        private static Result<InlineKeyboardMarkup> GetMarkupResult(List<SettingsCurrencyKeyboardData> data) => 
            Result.Success(new SettingsCurrencyKeyboard().ConcreteKeyboardMarkup(data));

        private async Task<Message> EditReplyMarkupAsync(
            long chatId, int messageId, InlineKeyboardMarkup markup, CancellationToken token) => 
            await Client.EditMessageReplyMarkupAsync(chatId, messageId, markup, token);
    }
}