using BetterTravel.Commands.Abstractions;

namespace BetterTravel.Commands.Telegram.Unsubscribe
{
    public class UnsubscribeCommand : ICommand
    {
        public long ChatId { get; set; }
    }
}