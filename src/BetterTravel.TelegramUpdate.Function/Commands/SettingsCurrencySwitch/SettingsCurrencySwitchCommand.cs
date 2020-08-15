using BetterTravel.Domain.Entities;
using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.TelegramUpdate.Function.Commands.SettingsCurrencySwitch
{
    public class SettingsCurrencySwitchCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
        public Currency Currency { get; set; }
    }
}