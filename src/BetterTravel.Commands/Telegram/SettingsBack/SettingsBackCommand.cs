using BetterTravel.Commands.Abstractions;

namespace BetterTravel.Commands.Telegram.SettingsBack
{
    public class SettingsBackCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
    }
}