using BetterTravel.Commands.Abstractions;

namespace BetterTravel.Commands.Telegram.SettingsSubscribe
{
    public class SettingsSubscribeCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
    }
}