using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.TelegramUpdate.Function.Commands.SettingsSubscriptionToggle
{
    public class SettingsSubscriptionToggleCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
    }
}