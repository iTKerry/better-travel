using BetterTravel.Domain.Entities.Base;
using BetterTravel.Domain.Entities.Enumerations;
using CSharpFunctionalExtensions;

namespace BetterTravel.Domain.Entities
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