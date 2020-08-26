using BetterTravel.DataAccess.Entities.Enumerations;
using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.Commands.Telegram.SettingsDepartureToggle
{
    public class SettingsDepartureToggleCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
        public DepartureLocation Departure { get; set; }
    }
}