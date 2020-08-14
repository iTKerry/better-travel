using AutoMapper;
using BetterTravel.Domain.Entities.Enumerations;
using BetterTravel.TelegramUpdate.Function.Commands.Settings;
using BetterTravel.TelegramUpdate.Function.Commands.SettingsBack;
using BetterTravel.TelegramUpdate.Function.Commands.SettingsCountries;
using BetterTravel.TelegramUpdate.Function.Commands.SettingsCountryToggle;
using BetterTravel.TelegramUpdate.Function.Commands.SettingsDepartures;
using BetterTravel.TelegramUpdate.Function.Commands.SettingsDepartureToggle;
using BetterTravel.TelegramUpdate.Function.Commands.SettingsSubscriptionToggle;
using BetterTravel.TelegramUpdate.Function.Commands.Start;
using Telegram.Bot.Types;

namespace BetterTravel.TelegramUpdate.Function.Mapper
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

        private static int ExtractCallbackQueryId(string data)
        {
            var idStr = data.Split(':')[1];
            return int.Parse(idStr);
        }
    }
}