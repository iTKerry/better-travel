using System.Collections.Generic;
using System.Linq;
using BetterTravel.Commands.Telegram.Keyboards.Data;
using BetterTravel.Commands.Telegram.SettingsBack;
using BetterTravel.Commands.Telegram.SettingsCurrencySwitch;
using Telegram.Bot.Types.ReplyMarkups;

namespace BetterTravel.Commands.Telegram.Keyboards.Factories
{
    public class SettingsCurrencyKeyboard : KeyboardFactoryBase<List<SettingsCurrencyKeyboardData>>
    {
        public override InlineKeyboardMarkup ConcreteKeyboardMarkup(List<SettingsCurrencyKeyboardData> data)
        {
            var lines = data
                .Select((d, idx) => new {Data = d, Index = idx})
                .GroupBy(t => t.Index / 1)
                .Select(g => g.Select(gi => gi.Data))
                .Select(GetCountryButtonsLines)
                .Append(Line(Button("<< Back", nameof(SettingsBackCommand))))
                .ToArray();
            var markup = Markup(lines);
            return new InlineKeyboardMarkup(markup);
        }

        private InlineKeyboardButton[] GetCountryButtonsLines(
            IEnumerable<SettingsCurrencyKeyboardData> countriesData) =>
            Line(countriesData.Select(GetCountryButton).ToArray());

        private InlineKeyboardButton GetCountryButton(SettingsCurrencyKeyboardData data) =>
            Button(
                data.IsSubscribed
                    ? $"\U00002714 {data.Name}"
                    : $"\U00002796 {data.Name}",
                $"{nameof(SettingsCurrencySwitchCommand)}:{data.Id}");
    }
}