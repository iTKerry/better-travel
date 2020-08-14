using System.Linq;
using BetterTravel.Domain.Entities.Base;
using BetterTravel.Domain.Entities.Enumerations;
using BetterTravel.Domain.Enums;
using BetterTravel.Domain.ValueObjects;
using CSharpFunctionalExtensions;

namespace BetterTravel.Domain.Entities
{
    public class Chat : AggregateRoot
    {
        protected Chat()
        {
        }
        
        private Chat(long chatId, ChatInfo info, ChatSettings settings)
        {
            ChatId = chatId;
            Info = info;
            Settings = settings;
        }

        public long ChatId { get; private set; }
        public virtual ChatSettings Settings { get; private set; }
        public virtual ChatInfo Info { get; private set; }

        public static Result<Chat> Create(long chatId, Maybe<ChatInfo> maybeInfo, Maybe<ChatSettings> maybeSettings)
        {
            var infoResult = maybeInfo.ToResult("Chat information not provided.");
            var settingsResult = maybeSettings.ToResult("Settings information not provided.");
            
            return Result
                .Combine(infoResult, settingsResult)
                .Bind(() => Result.Success(new Chat(chatId, infoResult.Value, settingsResult.Value)));
        }

        public Result ToggleSubscription() =>
            Settings.IsSubscribed
                ? Settings.Unsubscribe()
                : Settings.Subscribe();

        public Result UpdateInfo(string title, string description, ChatType type) =>
            ChatInfo
                .Create(title, description, type)
                .Tap(chatInfo => Info = chatInfo);

        public Result ToggleCountrySubscription(Country country) =>
            Settings.CountrySubscriptions.Any(cs => cs.Country == country)
                ? Settings.UnsubscribeFromCountry(country)
                : Settings.SubscribeToCountry(country);

        public Result ToggleDepartureSubscription(DepartureLocation departure) =>
            Settings.DepartureSubscriptions.Any(ds => ds.Departure == departure)
                ? Settings.UnsubscribeFromDeparture(departure)
                : Settings.SubscribeToDeparture(departure);
    }
}