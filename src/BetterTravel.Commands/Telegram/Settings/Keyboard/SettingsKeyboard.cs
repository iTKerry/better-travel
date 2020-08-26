using BetterTravel.Commands.Abstractions;
using BetterTravel.Commands.Telegram.SettingsCountries;
using BetterTravel.Commands.Telegram.SettingsCurrency;
using BetterTravel.Commands.Telegram.SettingsDepartures;
using BetterTravel.Commands.Telegram.SettingsSubscriptionToggle;
using Telegram.Bot.Types.ReplyMarkups;

namespace BetterTravel.Commands.Telegram.Settings.Keyboard
{
    public class SettingsKeyboard : KeyboardFactoryBase<SettingsKeyboardData>
    {
        public override InlineKeyboardMarkup ConcreteKeyboardMarkup(SettingsKeyboardData data) =>
            new InlineKeyboardMarkup(
                Markup(
                    Line(
                        Button(
                            data.IsSubscribed ? "Unsubscribe from updates" : "Subscribe to updates",
                            nameof(SettingsSubscriptionToggleCommand))
                    ),
                    Line(
                        Button(
                            "Change currency",
                            nameof(SettingsCurrencyCommand))
                    ),
                    Line(
                        Button(
                            "Countries",
                            nameof(SettingsCountriesCommand)),
                        Button(
                            "Departures",
                            nameof(SettingsDeparturesCommand))
                    )
                )
            );
    }
}