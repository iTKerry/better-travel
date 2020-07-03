using BetterTravel.Commands.Abstractions;

namespace BetterTravel.Commands.Telegram.SettingsDeparturesSubscribe
{
    public class SettingsDeparturesSubscribeCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
        public int DepartureId { get; set; }
    }
}