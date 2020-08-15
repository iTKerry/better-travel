using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.TelegramUpdate.Function.Commands.SettingsCurrency
{
    public class SettingsCurrencyCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
    }
}