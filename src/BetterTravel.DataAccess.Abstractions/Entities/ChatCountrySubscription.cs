using BetterExtensions.Domain.Base;
using BetterTravel.DataAccess.Abstractions.Entities.Enumerations;
using CSharpFunctionalExtensions;

namespace BetterTravel.DataAccess.Abstractions.Entities
{
    public class ChatCountrySubscription : Entity
    {
        protected ChatCountrySubscription()
        {
        }

        private ChatCountrySubscription(ChatSettings settings, Country country)
        {
            Settings = settings;
            Country = country;
        }
        
        public virtual ChatSettings Settings { get; private set; }
        public virtual Country Country { get; private set; }

        public static Result<ChatCountrySubscription> Create(Maybe<ChatSettings> maybeChatSettings, Maybe<Country> maybeCountry)
        {
            var chatSettingsResult = maybeChatSettings.ToResult("Settings was not provided.");
            var countryResult = maybeCountry.ToResult("Country was not provided.");

            return Result
                .Combine(chatSettingsResult, countryResult)
                .Bind(() => Result.Success(new ChatCountrySubscription(chatSettingsResult.Value, countryResult.Value)));
        }
    }
}