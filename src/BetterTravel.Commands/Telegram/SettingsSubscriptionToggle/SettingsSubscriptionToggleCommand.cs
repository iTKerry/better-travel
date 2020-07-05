using BetterTravel.Commands.Abstractions;

namespace BetterTravel.Commands.Telegram.SettingsSubscriptionToggle
{
    public class SettingsSubscriptionToggleCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
    }
}