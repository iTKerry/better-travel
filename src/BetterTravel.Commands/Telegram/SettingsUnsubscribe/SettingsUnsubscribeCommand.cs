using BetterTravel.Commands.Abstractions;

namespace BetterTravel.Commands.Telegram.SettingsUnsubscribe
{
    public class SettingsUnsubscribeCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
    }
}