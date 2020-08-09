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

namespace BetterTravel.Commands.Telegram.SettingsDepartures
{
    public class SettingsDeparturesCommandHandler : CommandHandlerBase<SettingsDeparturesCommand>
    {
        private const string KeyboardMessage = "You can choose the departures from which you will receive updates";

        private readonly ITelegramBotClient _telegram;
        
        public SettingsDeparturesCommandHandler(
            IUnitOfWork unitOfWork, 
            ITelegramBotClient telegram) : base(unitOfWork) =>
            _telegram = telegram;

        public override async Task<IHandlerResult> Handle(
            SettingsDeparturesCommand request, 
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

        private static Result<List<SettingsDepartureKeyboardData>> GetKeyboardDataResult(Chat chat) =>
            Result.Ok(DepartureLocation.AllDepartures
                .Select(departure => new SettingsDepartureKeyboardData
                {
                    Id = departure.Id,
                    Name = departure.Name,
                    IsSubscribed = chat.Settings.DepartureSubscriptions.Any(ds => ds.Departure == departure)
                }).ToList());

        private static Result<InlineKeyboardMarkup> GetMarkupResult(List<SettingsDepartureKeyboardData> data) => 
            Result.Ok(new SettingsDepartureKeyboard().ConcreteKeyboardMarkup(data));

        private async Task<Message> EditMessageTextAsync(
            long chatId, int messageId, string message, CancellationToken token) => 
            await _telegram.EditMessageTextAsync(chatId, messageId, message, cancellationToken: token);
        
        private async Task<Message> EditMessageReplyMarkupAsync(
            long chatId, int messageId, InlineKeyboardMarkup markup, CancellationToken token) => 
            await _telegram.EditMessageReplyMarkupAsync(chatId, messageId, markup, token);
    }
}