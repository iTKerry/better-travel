using System.Collections.Generic;
using System.Linq;
using BetterTravel.Commands.Telegram.Keyboards.Data;
using BetterTravel.Commands.Telegram.SettingsBack;
using BetterTravel.Commands.Telegram.SettingsDepartureToggle;
using Telegram.Bot.Types.ReplyMarkups;

namespace BetterTravel.Commands.Telegram.Keyboards.Factories
{
    public class SettingsDepartureKeyboard : KeyboardFactoryBase<List<SettingsDepartureKeyboardData>>
    {
        private const string KeyboardMessage = "Configure your subscription settings here";

        public override InlineKeyboardMarkup ConcreteKeyboardMarkup(List<SettingsDepartureKeyboardData> data)
        {
            var lines = data
                .Select((d, idx) => new {Data = d, Index = idx})
                .GroupBy(t => t.Index / 2)
                .Select(g => g.Select(gv => gv.Data))
                .Select(GetDeparturesButtonsLines)
                .Append(Line(Button("<< Back", nameof(SettingsBackCommand))))
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
                $"{nameof(SettingsDepartureToggleCommand)}:{data.Id}");
    }
}