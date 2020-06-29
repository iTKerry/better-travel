using System.Collections.Generic;
using System.Linq;
using BetterTravel.DataAccess.Abstraction.Entities.Base;
using CSharpFunctionalExtensions;

namespace BetterTravel.DataAccess.Abstraction.Entities
{
    public class ChatSettings : Entity
    {
        protected ChatSettings()
        {
        }
        
        private ChatSettings(bool isSubscribed) => 
            IsSubscribed = isSubscribed;

        public bool IsSubscribed { get; private set; }
        public int SettingsOfChatId { get; private set; }
        public virtual Chat Chat { get; private set; }
        
        private readonly List<SettingsCountry> _settingsCountries = new List<SettingsCountry>();
        public virtual IReadOnlyList<SettingsCountry> SettingsCountries => _settingsCountries.ToList();

        public static Result<ChatSettings> Create(bool isSubscribed)
        {
            var chatSettings = new ChatSettings(isSubscribed);
            return Result.Ok(chatSettings);
        }

        public Result SubscribeToCountry(Country country) =>
            Result
                .FailureIf(_settingsCountries.Any(c => c == country), "Already subscribed to this country.")
                .Bind(() => SettingsCountry.Create(this, country))
                .Tap(sc => _settingsCountries.Add(sc));
        
        public Result UnsubscribeFromCountry(Country country) =>
            Result
                .SuccessIf(_settingsCountries.Any(c => c == country), "You were not subscribed to this country.")
                .Bind(() => Maybe<SettingsCountry>
                    .From(_settingsCountries.FirstOrDefault(c => c == country))
                    .ToResult("Such country was not found in settings."))
                .Tap(sc => _settingsCountries.Remove(sc));

        public Result Subscribe() =>
            Result
                .FailureIf(IsSubscribed, "You already subscribed.")
                .Tap(() => IsSubscribed = true);

        public Result Unsubscribe() =>
            Result
                .SuccessIf(IsSubscribed, "You are not subscribed anyway.")
                .Tap(() => IsSubscribed = false);
    }
}