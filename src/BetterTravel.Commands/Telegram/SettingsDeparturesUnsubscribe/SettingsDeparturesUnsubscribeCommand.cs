using BetterTravel.Commands.Abstractions;

namespace BetterTravel.Commands.Telegram.SettingsDeparturesUnsubscribe
{
    public class SettingsDeparturesUnsubscribeCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
        public int DepartureId { get; set; }
    }
}