using BetterTravel.Commands.Abstractions;

namespace BetterTravel.Commands.Telegram.SettingsCountriesUnsubscribe
{
    public class SettingsCountriesUnsubscribeCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
        public int CountryId { get; set; }
    }
}