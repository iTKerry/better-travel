using BetterTravel.Commands.Abstractions;

namespace BetterTravel.Commands.Telegram.Subscribe
{
    public class SubscribeCommand : ICommand
    {
        public long ChatId { get; set; }
        public bool IsBot { get; set; }
    }
}