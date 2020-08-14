using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.TelegramUpdate.Function.Commands.SettingsCountries
{
    public class SettingsCountriesCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
    }
}