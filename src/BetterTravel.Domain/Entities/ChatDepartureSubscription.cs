using BetterTravel.Domain.Entities.Base;
using BetterTravel.Domain.Entities.Enumerations;
using CSharpFunctionalExtensions;

namespace BetterTravel.Domain.Entities
{
    public class ChatDepartureSubscription : Entity
    {
        protected ChatDepartureSubscription()
        {
        }

        private ChatDepartureSubscription(ChatSettings settings, DepartureLocation departure)
        {
            Settings = settings;
            Departure = departure;
        }
        
        public virtual ChatSettings Settings { get; private set; }
        public virtual DepartureLocation Departure { get; private set; }

        public static Result<ChatDepartureSubscription> Create(
            Maybe<ChatSettings> maybeChatSettings, 
            Maybe<DepartureLocation> maybeDeparture)
        {
            var chatSettingsResult = maybeChatSettings.ToResult("Settings was not provided.");
            var departureResult = maybeDeparture.ToResult("Country was not provided.");

            return Result
                .Combine(chatSettingsResult, departureResult)
                .Bind(() => Result.Ok(new ChatDepartureSubscription(chatSettingsResult.Value, departureResult.Value)));
        }
    }
}