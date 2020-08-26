using BetterTravel.DataAccess.Entities.Enumerations;
using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.Commands.Telegram.SettingsCountryToggle
{
    public class SettingsCountryToggleCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
        public Country Country { get; set; }
    }
}