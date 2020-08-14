using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.TelegramUpdate.Function.Commands.SettingsDepartures
{
    public class SettingsDeparturesCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
    }
}