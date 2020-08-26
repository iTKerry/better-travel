using AutoMapper;
using BetterTravel.Commands.Telegram.Settings;
using BetterTravel.Commands.Telegram.SettingsBack;
using BetterTravel.Commands.Telegram.SettingsCountries;
using BetterTravel.Commands.Telegram.SettingsCountryToggle;
using BetterTravel.Commands.Telegram.SettingsCurrency;
using BetterTravel.Commands.Telegram.SettingsCurrencySwitch;
using BetterTravel.Commands.Telegram.SettingsDepartures;
using BetterTravel.Commands.Telegram.SettingsDepartureToggle;
using BetterTravel.Commands.Telegram.SettingsSubscriptionToggle;
using BetterTravel.Commands.Telegram.Start;
using BetterTravel.DataAccess.Entities;
using BetterTravel.DataAccess.Entities.Enumerations;
using Telegram.Bot.Types;

namespace BetterTravel.Api.Requests.Profiles
{
    public class TelegramProfile : Profile
    {
        public TelegramProfile()
        {
            CreateMap<Message, StartCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Chat.Id))
                .ForMember(c => c.IsBot, exp => exp.MapFrom(f => f.From.IsBot))
                .ForMember(c => c.Type, exp => exp.MapFrom(f => f.Chat.Type))
                .ForMember(c => c.Title, exp => exp.MapFrom(f => f.Chat.Title))
                .ForMember(c => c.Description, exp => exp.MapFrom(f => f.Chat.Description));
            
            CreateMap<Message, SettingsCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Chat.Id));
            
            CreateMap<CallbackQuery, SettingsSubscriptionToggleCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.Message.MessageId));
            
            CreateMap<CallbackQuery, SettingsBackCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.Message.MessageId));

            CreateMap<CallbackQuery, SettingsDeparturesCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.Message.MessageId));

            CreateMap<CallbackQuery, SettingsCountriesCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.Message.MessageId));

            CreateMap<CallbackQuery, SettingsCurrencyCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.Message.MessageId));
            
            CreateMap<CallbackQuery, SettingsCurrencySwitchCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.Message.MessageId))
                .AfterMap(CurrencySwitchAfterMap);
            
            CreateMap<CallbackQuery, SettingsCountryToggleCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.Message.MessageId))
                .AfterMap(CountryToggleAfterMap);

            CreateMap<CallbackQuery, SettingsDepartureToggleCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.Message.MessageId))
                .AfterMap(DepartureToggleAfterMap);
        }

        private static void CountryToggleAfterMap(CallbackQuery callbackQuery, SettingsCountryToggleCommand command)
        {
            var countryId = ExtractCallbackQueryId(callbackQuery.Data);
            command.Country = Country.FromId(countryId);
        }

        private static void DepartureToggleAfterMap(CallbackQuery callbackQuery, SettingsDepartureToggleCommand command)
        {
            var departureId = ExtractCallbackQueryId(callbackQuery.Data);
            command.Departure = DepartureLocation.FromId(departureId);
        }

        private static void CurrencySwitchAfterMap(CallbackQuery callbackQuery, SettingsCurrencySwitchCommand command)
        {
            var currencyId = ExtractCallbackQueryId(callbackQuery.Data);
            command.Currency = Currency.FromId(currencyId);
        }

        private static int ExtractCallbackQueryId(string data)
        {
            var idStr = data.Split(':')[1];
            return int.Parse(idStr);
        }
    }
}