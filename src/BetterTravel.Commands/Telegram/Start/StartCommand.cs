using BetterTravel.Commands.Abstractions;

namespace BetterTravel.Commands.Telegram.Start
{
    public class StartCommand : ICommand
    {
        public long ChatId { get; set; }
        public bool IsBot { get; set; }
    }
}