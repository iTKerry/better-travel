using System.Collections.Generic;
using System.Linq;
using BetterTravel.TelegramUpdate.Function.Keyboards.Data;
using Telegram.Bot.Types.ReplyMarkups;

namespace BetterTravel.TelegramUpdate.Function.Keyboards.Factories
{
    public class SettingsDepartureKeyboard : KeyboardFactoryBase<List<SettingsDepartureKeyboardData>>
    {
        private const string KeyboardMessage = "Configure your subscription settings here";

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
                    ? $"\U00002714 {data.Name}"
                    : $"\U00002796 {data.Name}",
                $"SettingsDepartureToggle:{data.Id}");
    }
}