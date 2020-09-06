using BetterTravel.DataAccess.Entities;
using BetterTravel.DataAccess.Entities.Enumerations;
using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.Commands.Telegram.SettingsCurrencySwitch
{
    public class SettingsCurrencySwitchCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
        public Currency Currency { get; set; }
    }
}