using BetterTravel.Application.Keyboards.Data;
using Telegram.Bot.Types.ReplyMarkups;

namespace BetterTravel.Application.Keyboards.Factories
{
    public class SettingsKeyboardFactoryBaseKeyboard : KeyboardFactoryBase<SettingsKeyboardData>
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
}