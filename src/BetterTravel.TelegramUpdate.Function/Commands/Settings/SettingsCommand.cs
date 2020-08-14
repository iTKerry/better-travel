using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.TelegramUpdate.Function.Commands.Settings
{
    public class SettingsCommand : ICommand
    {
        public long ChatId { get; set; }
    }
}