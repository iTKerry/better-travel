using BetterTravel.Commands.Abstractions;
using BetterTravel.DataAccess.Abstraction.Entities.Enums;

namespace BetterTravel.Commands.Telegram.Subscribe
{
    public class SubscribeCommand : ICommand
    {
        public long ChatId { get; set; }
        public bool IsBot { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ChatType Type { get; set; }
    }
}