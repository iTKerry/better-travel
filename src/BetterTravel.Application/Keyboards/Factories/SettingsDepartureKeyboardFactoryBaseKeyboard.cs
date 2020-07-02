using System.Collections.Generic;
using System.Linq;
using BetterTravel.Application.Keyboards.Data;
using Telegram.Bot.Types.ReplyMarkups;

namespace BetterTravel.Application.Keyboards.Factories
{
    public class SettingsDepartureKeyboardFactoryBaseKeyboard : KeyboardFactoryBase<List<SettingsDepartureKeyboardData>>
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