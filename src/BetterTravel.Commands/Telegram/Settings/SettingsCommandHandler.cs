using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Commands.Abstractions;
using BetterTravel.DataAccess.Repositories;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using CSharpFunctionalExtensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Chat = BetterTravel.DataAccess.Entities.Chat;

namespace BetterTravel.Commands.Telegram.Settings
{
    public class SettingsCommandHandler : CommandHandlerBase<SettingsCommand>
    {
        private readonly ITelegramBotClient _telegram;
        
        public SettingsCommandHandler(
            IUnitOfWork unitOfWork, 
            ITelegramBotClient telegram) : base(unitOfWork) =>
            _telegram = telegram;

        public override async Task<IHandlerResult> Handle(
            SettingsCommand request, 
            CancellationToken cancellationToken) =>
            await UnitOfWork.ChatRepository
                .GetFirstAsync(c => c.ChatId == request.ChatId)
                .ToResult("That chat wasn't found between our subscribers.")
                .Bind(GetKeyboardDataResult)
                .Bind(GetMarkupResult)
                .Tap(markup => SendMessageAsync(request.ChatId, "Settings message", markup, cancellationToken))
                .Finally(result => result.IsFailure
                    ? ValidationFailed(result.Error)
                    : Ok());

        private static Result<SettingsKeyboardData> GetKeyboardDataResult(Chat chat) => 
            Result.Ok(new SettingsKeyboardData {IsSubscribed = chat.Settings.IsSubscribed});
        
        private static Result<InlineKeyboardMarkup> GetMarkupResult(SettingsKeyboardData data) => 
            Result.Ok(new SettingsKeyboard().ConcreteKeyboardMarkup(data));

        private async Task<Message> SendMessageAsync(
            long chatId, string message, InlineKeyboardMarkup markup, CancellationToken token) => 
            await _telegram.SendTextMessageAsync(chatId, message, replyMarkup: markup, cancellationToken: token);
    }

    public abstract class KeyboardFactory<T> where T : class
    {
        public abstract InlineKeyboardMarkup ConcreteKeyboardMarkup(T departuresData);

        protected InlineKeyboardButton[][] Markup(params InlineKeyboardButton[][] lines) =>
            lines;
        
        protected InlineKeyboardButton[] Line(params InlineKeyboardButton[] buttons) =>
            buttons;
        
        protected InlineKeyboardButton Button(string text, string callbackData) =>
            new InlineKeyboardButton {Text = text, CallbackData = callbackData};
    }

    public class SettingsKeyboardData
    {
        public bool IsSubscribed { get; set; }
    }

    public class SettingsKeyboard : KeyboardFactory<SettingsKeyboardData>
    {
        public override InlineKeyboardMarkup ConcreteKeyboardMarkup(SettingsKeyboardData departuresData)
        {
            var markup = Markup(
                Line(
                    Button(
                        departuresData.IsSubscribed ? "Unsubscribe" : "Subscribe",
                        departuresData.IsSubscribed ? "SettingsUnsubscribe" : "SettingsSubscribe")),
                Line(
                    Button("Countries", "SettingsCountries"),
                    Button("Departures", "SettingsDepartures"))
            );
            
            return new InlineKeyboardMarkup(markup);
        }
    }

    public class SettingsCountryKeyboardData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSubscribed { get; set; }
    }
    
    public class SettingsCountryKeyboard : KeyboardFactory<List<SettingsCountryKeyboardData>>
    {
        public override InlineKeyboardMarkup ConcreteKeyboardMarkup(List<SettingsCountryKeyboardData> departuresData)
        {
            var lines = departuresData
                .Select((data, idx) => new {Data = data, Index = idx})
                .GroupBy(t => t.Index / 2)
                .Select(g => g.Select(gv => gv.Data))
                .Select(GetCountryButtonsLines)
                .Append(Line(Button("<< Back", "SettingsBack")))
                .ToArray();
            var markup = Markup(lines);
            return new InlineKeyboardMarkup(markup);
        }

        private InlineKeyboardButton[] GetCountryButtonsLines(
            IEnumerable<SettingsCountryKeyboardData> countriesData) =>
            Line(countriesData.Select(GetCountryButton).ToArray());

        private InlineKeyboardButton GetCountryButton(SettingsCountryKeyboardData data) =>
            Button(
                data.IsSubscribed
                    ? $"- {data.Name}"
                    : $"+ {data.Name}",
                data.IsSubscribed
                    ? $"SettingsCountryUnsubscribe:{data.Id}"
                    : $"SettingsCountrySubscribe:{data.Id}");
    }

    public class SettingsDepartureKeyboardData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSubscribed { get; set; }
    }
    
    public class SettingsDepartureKeyboard : KeyboardFactory<List<SettingsDepartureKeyboardData>>
    {
        public override InlineKeyboardMarkup ConcreteKeyboardMarkup(List<SettingsDepartureKeyboardData> departuresData)
        {
            var lines = departuresData
                .Select((data, idx) => new {Data = data, Index = idx})
                .GroupBy(t => t.Index / 2)
                .Select(g => g.Select(gv => gv.Data))
                .Select(GetDeparturesButtonsLines)
                .Append(Line(Button("<< Back", "SettingsBack")))
                .ToArray();
            var markup = Markup(lines);
            return new InlineKeyboardMarkup(markup);
        }

        private InlineKeyboardButton[] GetDeparturesButtonsLines(
            IEnumerable<SettingsDepartureKeyboardData> departuresData) =>
            Line(departuresData.Select(GetDepartureButton).ToArray());

        private InlineKeyboardButton GetDepartureButton(SettingsDepartureKeyboardData data) =>
            Button(
                data.IsSubscribed
                    ? $"- {data.Name}"
                    : $"+ {data.Name}",
                data.IsSubscribed
                    ? $"SettingsCountryUnsubscribe:{data.Id}"
                    : $"SettingsCountrySubscribe:{data.Id}");
    }
}