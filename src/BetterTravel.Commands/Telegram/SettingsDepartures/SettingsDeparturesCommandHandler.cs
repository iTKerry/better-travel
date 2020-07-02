using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Commands.Abstractions;
using BetterTravel.Commands.Telegram.Settings;
using BetterTravel.DataAccess.Entities;
using BetterTravel.DataAccess.Repositories;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using CSharpFunctionalExtensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Chat = BetterTravel.DataAccess.Entities.Chat;

namespace BetterTravel.Commands.Telegram.SettingsDepartures
{
    public class SettingsDeparturesCommandHandler : CommandHandlerBase<SettingsDeparturesCommand>
    {
        private readonly ITelegramBotClient _telegram;
        
        public SettingsDeparturesCommandHandler(
            IUnitOfWork unitOfWork, 
            ITelegramBotClient telegram) : base(unitOfWork) =>
            _telegram = telegram;

        public override async Task<IHandlerResult> Handle(
            SettingsDeparturesCommand request, 
            CancellationToken cancellationToken) =>
            await UnitOfWork.ChatRepository
                .GetFirstAsync(c => c.ChatId == request.ChatId)
                .ToResult("That chat wasn't found between our subscribers.")
                .Bind(GetKeyboardDataResult)
                .Bind(GetMarkupResult)
                .Tap(markup => EditMessageReplyMarkupAsync(request.ChatId, request.MessageId, markup, cancellationToken))
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

        private async Task<Message> EditMessageReplyMarkupAsync(
            long chatId, int messageId, InlineKeyboardMarkup markup, CancellationToken token) => 
            await _telegram.EditMessageReplyMarkupAsync(chatId, messageId, markup, token);
    }
}