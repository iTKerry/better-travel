using System.Collections.Generic;
using System.Linq;
using BetterTravel.Domain.Entities.Base;
using BetterTravel.Domain.Entities.Enumerations;
using CSharpFunctionalExtensions;

namespace BetterTravel.Domain.Entities
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
            return Result.Success(chatSettings);
        }

        public Result SubscribeToCountry(Country country) =>
            Result
                .FailureIf(
                    _countrySubscriptions.Any(s => s.Country == country), 
                    "Already subscribed to this country.")
                .Bind(() => ChatCountrySubscription.Create(this, country))
                .Tap(sc => _countrySubscriptions.Add(sc));
        
        public Result UnsubscribeFromCountry(Country country) =>
            Result
                .SuccessIf(
                    _countrySubscriptions.Any(s => s.Country == country),
                    _countrySubscriptions.FirstOrDefault(c => c.Country == country),
                    "You were not subscribed to this country.")
                .Tap(sc => _countrySubscriptions.Remove(sc));

        public Result SubscribeToDeparture(DepartureLocation departure) =>
            Result
                .FailureIf(
                    _departureSubscriptions.Any(s => s.Departure == departure), 
                    "Already subscribed to this departure.")
                .Bind(() => ChatDepartureSubscription.Create(this, departure))
                .Tap(sc => _departureSubscriptions.Add(sc));
        
        public Result UnsubscribeFromDeparture(DepartureLocation departure) =>
            Result
                .SuccessIf(
                    _departureSubscriptions.Any(s => s.Departure == departure),
                    _departureSubscriptions.FirstOrDefault(s => s.Departure == departure),
                    "You were not subscribed to this departure.")
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