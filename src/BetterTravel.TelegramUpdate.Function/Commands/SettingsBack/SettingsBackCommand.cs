using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.TelegramUpdate.Function.Commands.SettingsBack
{
    public class SettingsBackCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
    }
}