using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Application.Exchange.Abstractions;
using BetterTravel.Commands.Abstractions;
using BetterTravel.Commands.Telegram.SettingsCurrency.Keyboard;
using BetterTravel.DataAccess.Abstractions.Entities.Enumerations;
using BetterTravel.DataAccess.Abstractions.Enums;
using BetterTravel.DataAccess.Abstractions.Repository;
using BetterTravel.MediatR.Core.Abstractions;
using CSharpFunctionalExtensions;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Chat = BetterTravel.DataAccess.Abstractions.Entities.Chat;

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

        public override async Task<IHandlerResult> Handle(
            SettingsCurrencySwitchCommand request, 
            CancellationToken ctx)
        {
            Maybe<Chat> maybeChat = await UnitOfWork.ChatWriteRepository
                .GetFirstAsync(c => c.ChatId == request.ChatId);
            
            return await maybeChat
                .ToResult("That chat wasn't found between our subscribers.")
                .Tap(chat => chat.ChangeCurrency(request.Currency))
                .Tap(chat => UnitOfWork.ChatWriteRepository.Save(chat))
                .Bind(GetKeyboardDataResult)
                .Bind(GetMarkupResult)
                .Tap(markup => EditReplyMarkupAsync(request.ChatId, request.MessageId, markup, ctx))
                .Finally(result => result.IsFailure
                    ? ValidationFailed(result.Error)
                    : Ok());
        }

        private async Task<Result<List<SettingsCurrencyKeyboardData>>> GetKeyboardDataResult(Chat chat) =>
            (await _exchange.GetExchangeAsync())
            .Map(rates => Currency.AllCurrencies
                .Select(currency => new SettingsCurrencyKeyboardData
                {
                    Id = currency.Id,
                    Name = currency.IsType(CurrencyType.UAH)
                        ? $"{currency.Code} {currency.Sign}"
                        : rates.First(rate => currency.IsType(rate.Type)).ToString(),
                    IsSubscribed = chat.Settings.Currency == currency
                }).ToList());

        private static Result<InlineKeyboardMarkup> GetMarkupResult(List<SettingsCurrencyKeyboardData> data) => 
            Result.Success(new SettingsCurrencyKeyboard().ConcreteKeyboardMarkup(data));
    }
}