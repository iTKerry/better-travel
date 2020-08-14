using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.Domain.Entities.Enumerations;
using BetterTravel.MediatR.Core.Abstractions;
using BetterTravel.TelegramUpdate.Function.Commands.Abstractions;
using BetterTravel.TelegramUpdate.Function.Keyboards.Data;
using BetterTravel.TelegramUpdate.Function.Keyboards.Factories;
using CSharpFunctionalExtensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Chat = BetterTravel.Domain.Entities.Chat;

namespace BetterTravel.TelegramUpdate.Function.Commands.SettingsDepartures
{
    public class SettingsDeparturesCommandHandler : TelegramCommandHandler<SettingsDeparturesCommand>
    {
        private const string KeyboardMessage = "You can choose the departures from which you will receive updates";

        public SettingsDeparturesCommandHandler(
            IUnitOfWork unitOfWork, ITelegramBotClient telegram)
            : base(unitOfWork, telegram) { }

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
            Result
                .Success(DepartureLocation.AllDepartures
                .Select(departure => new SettingsDepartureKeyboardData
                {
                    Id = departure.Id,
                    Name = departure.Name,
                    IsSubscribed = chat.Settings.DepartureSubscriptions.Any(ds => ds.Departure == departure)
                }).ToList());

        private static Result<InlineKeyboardMarkup> GetMarkupResult(List<SettingsDepartureKeyboardData> data) => 
            Result.Success(new SettingsDepartureKeyboard().ConcreteKeyboardMarkup(data));

        private async Task<Message> EditMessageTextAsync(
            long chatId, int messageId, string message, CancellationToken token) => 
            await Client.EditMessageTextAsync(chatId, messageId, message, cancellationToken: token);
        
        private async Task<Message> EditMessageReplyMarkupAsync(
            long chatId, int messageId, InlineKeyboardMarkup markup, CancellationToken token) => 
            await Client.EditMessageReplyMarkupAsync(chatId, messageId, markup, token);
    }
}