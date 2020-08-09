using BetterTravel.Commands.Abstractions;
using BetterTravel.Domain.Entities.Enumerations;

namespace BetterTravel.Commands.Telegram.SettingsDepartureToggle
{
    public class SettingsDepartureToggleCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
        public DepartureLocation Departure { get; set; }
    }
}