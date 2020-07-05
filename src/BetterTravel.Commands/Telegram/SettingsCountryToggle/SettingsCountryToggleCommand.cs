using BetterTravel.Commands.Abstractions;
using BetterTravel.DataAccess.Entities;

namespace BetterTravel.Commands.Telegram.SettingsCountryToggle
{
    public class SettingsCountryToggleCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
        public Country Country { get; set; }
    }
}