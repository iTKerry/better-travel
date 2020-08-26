using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.Commands.Telegram.Settings
{
    public class SettingsCommand : ICommand
    {
        public long ChatId { get; set; }
    }
}