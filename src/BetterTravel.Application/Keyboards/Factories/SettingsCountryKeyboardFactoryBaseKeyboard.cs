using System.Collections.Generic;
using System.Linq;
using BetterTravel.Application.Keyboards.Data;
using Telegram.Bot.Types.ReplyMarkups;

namespace BetterTravel.Application.Keyboards.Factories
{
    public class SettingsCountryKeyboardFactoryBaseKeyboard : KeyboardFactoryBase<List<SettingsCountryKeyboardData>>
    {
        public override InlineKeyboardMarkup ConcreteKeyboardMarkup(List<SettingsCountryKeyboardData> departuresData)
        {
            var lines = departuresData
                .Select((data, idx) => new {Data = data, Index = idx})
                .GroupBy(t => t.Index / 2)
                .Select(g => g.Select(gi => gi.Data))
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
}