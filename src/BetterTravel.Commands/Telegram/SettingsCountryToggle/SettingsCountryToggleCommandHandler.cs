using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Application.Keyboards.Data;
using BetterTravel.Application.Keyboards.Factories;
using BetterTravel.Commands.Abstractions;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.Domain.Entities.Enumerations;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using CSharpFunctionalExtensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Chat = BetterTravel.Domain.Entities.Chat;

namespace BetterTravel.Commands.Telegram.SettingsCountryToggle
{
    public class SettingsCountryToggleCommandHandler : CommandHandlerBase<SettingsCountryToggleCommand>
    {
        private readonly ITelegramBotClient _telegram;
        
        public SettingsCountryToggleCommandHandler(
            IUnitOfWork unitOfWork, 
            ITelegramBotClient telegram) : base(unitOfWork) =>
            _telegram = telegram;

        public override async Task<IHandlerResult> Handle(
            SettingsCountryToggleCommand request,
            CancellationToken ctx) =>
            await UnitOfWork.ChatRepository
                .GetFirstAsync(c => c.ChatId == request.ChatId)
                .ToResult("That chat wasn't found between our subscribers.")
                .Tap(chat => chat.ToggleCountrySubscription(request.Country))
                .Tap(chat => UnitOfWork.ChatRepository.Save(chat))
                .Tap(() => UnitOfWork.CommitAsync())
                .Bind(GetKeyboardDataResult)
                .Bind(GetMarkupResult)
                .Tap(markup => EditReplyMarkupAsync(request.ChatId, request.MessageId, markup, ctx))
                .Finally(result => result.IsFailure
                    ? ValidationFailed(result.Error)
                    : Ok());

        private static Result<List<SettingsCountryKeyboardData>> GetKeyboardDataResult(Chat chat) =>
            Result.Ok(Country.AllCountries
                .Select(country => new SettingsCountryKeyboardData
                {
                    Id = country.Id,
                    Name = country.Name,
                    IsSubscribed = chat.Settings.CountrySubscriptions.Any(cs => cs.Country == country)
                }).ToList());

        private static Result<InlineKeyboardMarkup> GetMarkupResult(List<SettingsCountryKeyboardData> data) => 
            Result.Ok(new SettingsCountryKeyboard().ConcreteKeyboardMarkup(data));

        private async Task<Message> EditReplyMarkupAsync(
            long chatId, int messageId, InlineKeyboardMarkup markup, CancellationToken token) => 
            await _telegram.EditMessageReplyMarkupAsync(chatId, messageId, markup, token);
    }
}