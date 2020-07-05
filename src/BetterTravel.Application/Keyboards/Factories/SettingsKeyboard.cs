using BetterTravel.Application.Keyboards.Data;
using Telegram.Bot.Types.ReplyMarkups;

namespace BetterTravel.Application.Keyboards.Factories
{
    public class SettingsKeyboard : KeyboardFactoryBase<SettingsKeyboardData>
    {
        public override InlineKeyboardMarkup ConcreteKeyboardMarkup(SettingsKeyboardData departuresData)
        {
            var markup = Markup(
                Line(
                    Button(
                        departuresData.IsSubscribed ? "Unsubscribe" : "Subscribe",
                        "SettingsSubscriptionToggle")),
                Line(
                    Button("Countries", "SettingsCountries"),
                    Button("Departures", "SettingsDepartures"))
            );
            
            return new InlineKeyboardMarkup(markup);
        }
    }
}