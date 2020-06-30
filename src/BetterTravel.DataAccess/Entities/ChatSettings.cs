using System.Collections.Generic;
using System.Linq;
using BetterTravel.DataAccess.Entities.Base;
using CSharpFunctionalExtensions;

namespace BetterTravel.DataAccess.Entities
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
        
        private readonly List<ChatCountrySubscription> _countrySubscriptions = new List<ChatCountrySubscription>();
        public virtual IReadOnlyList<ChatCountrySubscription> CountrySubscriptions => _countrySubscriptions.ToList();

        private readonly List<ChatDepartureSubscription> _departureSubscriptions = new List<ChatDepartureSubscription>();
        public virtual IReadOnlyList<ChatDepartureSubscription> DepartureSubscriptions => _departureSubscriptions.ToList();
        
        public static Result<ChatSettings> Create(bool isSubscribed)
        {
            var chatSettings = new ChatSettings(isSubscribed);
            return Result.Ok(chatSettings);
        }

        public Result SubscribeToCountry(Country country) =>
            Result
                .FailureIf(_countrySubscriptions.Any(c => c == country), "Already subscribed to this country.")
                .Bind(() => ChatCountrySubscription.Create(this, country))
                .Tap(sc => _countrySubscriptions.Add(sc));
        
        public Result UnsubscribeFromCountry(Country country) =>
            Result
                .SuccessIf(_countrySubscriptions.Any(c => c == country), "You were not subscribed to this country.")
                .Bind(() => Maybe<ChatCountrySubscription>
                    .From(_countrySubscriptions.FirstOrDefault(c => c == country))
                    .ToResult("Such country was not found in settings."))
                .Tap(sc => _countrySubscriptions.Remove(sc));

        public Result SubscribeToDeparture(DepartureLocation departure) =>
            Result
                .FailureIf(_departureSubscriptions.Any(c => c == departure), "Already subscribed to this departure.")
                .Bind(() => ChatDepartureSubscription.Create(this, departure))
                .Tap(sc => _departureSubscriptions.Add(sc));
        
        public Result UnsubscribeFromDeparture(DepartureLocation departure) =>
            Result
                .SuccessIf(_departureSubscriptions.Any(c => c == departure), "You were not subscribed to this departure.")
                .Bind(() => Maybe<ChatDepartureSubscription>
                    .From(_departureSubscriptions.FirstOrDefault(c => c == departure))
                    .ToResult("Such country was not found in settings."))
                .Tap(sc => _departureSubscriptions.Remove(sc));
        
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