using BetterTravel.Domain.Entities.Enumerations;
using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.TelegramUpdate.Function.Commands.SettingsDepartureToggle
{
    public class SettingsDepartureToggleCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
        public DepartureLocation Departure { get; set; }
    }
}