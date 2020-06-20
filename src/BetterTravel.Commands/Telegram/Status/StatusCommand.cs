using BetterTravel.Commands.Abstractions;

namespace BetterTravel.Commands.Telegram.Status
{
    public class StatusCommand : ICommand
    {
        public long ChatId { get; set; }
    }
}