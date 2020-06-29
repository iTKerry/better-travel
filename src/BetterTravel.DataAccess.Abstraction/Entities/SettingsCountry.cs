using BetterTravel.DataAccess.Abstraction.Entities.Base;
using CSharpFunctionalExtensions;

namespace BetterTravel.DataAccess.Abstraction.Entities
{
    public class SettingsCountry : Entity
    {
        protected SettingsCountry()
        {
        }

        private SettingsCountry(ChatSettings settings, Country country)
        {
            Settings = settings;
            Country = country;
        }
        
        public virtual ChatSettings Settings { get; private set; }
        public virtual Country Country { get; private set; }

        public static Result<SettingsCountry> Create(Maybe<ChatSettings> maybeChatSettings, Maybe<Country> maybeCountry)
        {
            var chatSettingsResult = maybeChatSettings.ToResult("Settings was not provided.");
            var countryResult = maybeCountry.ToResult("Country was not provided.");

            return Result
                .Combine(chatSettingsResult, countryResult)
                .Bind(() => Result.Ok(new SettingsCountry(chatSettingsResult.Value, countryResult.Value)));
        }
    }
}