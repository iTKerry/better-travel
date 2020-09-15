using BetterExtensions.Domain.Base;
using BetterTravel.DataAccess.Abstractions.Entities.Enumerations;
using CSharpFunctionalExtensions;

namespace BetterTravel.DataAccess.Abstractions.Entities
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
                .Bind(() => Result.Success(new ChatDepartureSubscription(chatSettingsResult.Value, departureResult.Value)));
        }
    }
}