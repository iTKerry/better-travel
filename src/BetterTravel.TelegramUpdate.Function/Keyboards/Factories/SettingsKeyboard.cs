using BetterTravel.TelegramUpdate.Function.Commands.SettingsCountries;
using BetterTravel.TelegramUpdate.Function.Commands.SettingsCurrency;
using BetterTravel.TelegramUpdate.Function.Commands.SettingsDepartures;
using BetterTravel.TelegramUpdate.Function.Commands.SettingsSubscriptionToggle;
using BetterTravel.TelegramUpdate.Function.Keyboards.Data;
using Telegram.Bot.Types.ReplyMarkups;

namespace BetterTravel.TelegramUpdate.Function.Keyboards.Factories
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