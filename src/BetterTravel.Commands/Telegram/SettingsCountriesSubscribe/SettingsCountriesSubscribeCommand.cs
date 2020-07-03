using BetterTravel.Commands.Abstractions;

namespace BetterTravel.Commands.Telegram.SettingsCountriesSubscribe
{
    public class SettingsCountriesSubscribeCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
        public int CountryId { get; set; }
    }
}