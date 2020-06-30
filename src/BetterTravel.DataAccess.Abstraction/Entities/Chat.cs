using BetterTravel.DataAccess.Abstraction.Entities.Base;
using BetterTravel.DataAccess.Abstraction.Entities.Enums;
using BetterTravel.DataAccess.Abstraction.ValueObjects;
using CSharpFunctionalExtensions;

namespace BetterTravel.DataAccess.Abstraction.Entities
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
                .Bind(() => Result.Ok(new Chat(chatId, infoResult.Value, settingsResult.Value)));
        }

        public Result Subscribe() =>
            Settings.Subscribe();

        public Result SubscribeToCountry(Country country) =>
            Settings.SubscribeToCountry(country);

        public Result SubscribeToDeparture(DepartureLocation departure) =>
            Settings.SubscribeToDeparture(departure);
        
        public Result Unsubscribe() =>
            Settings.Unsubscribe();

        public Result UnsubscribeFromCountry(Country country) =>
            Settings.UnsubscribeFromCountry(country);

        public Result UnsubscribeFromDeparture(DepartureLocation departure) =>
            Settings.UnsubscribeFromDeparture(departure);

        public Result UpdateInfo(string title, string description, ChatType type) =>
            ChatInfo
                .Create(title, description, type)
                .Tap(chatInfo => Info = chatInfo);
    }
}