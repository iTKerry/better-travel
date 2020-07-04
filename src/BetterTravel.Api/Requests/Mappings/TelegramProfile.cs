using AutoMapper;
using BetterTravel.Commands.Telegram.Settings;
using BetterTravel.Commands.Telegram.SettingsBack;
using BetterTravel.Commands.Telegram.SettingsCountries;
using BetterTravel.Commands.Telegram.SettingsCountriesSubscribe;
using BetterTravel.Commands.Telegram.SettingsCountriesUnsubscribe;
using BetterTravel.Commands.Telegram.SettingsDepartures;
using BetterTravel.Commands.Telegram.SettingsDeparturesSubscribe;
using BetterTravel.Commands.Telegram.SettingsDeparturesUnsubscribe;
using BetterTravel.Commands.Telegram.SettingsSubscribe;
using BetterTravel.Commands.Telegram.SettingsUnsubscribe;
using BetterTravel.Commands.Telegram.Start;
using Telegram.Bot.Types;

namespace BetterTravel.Api.Requests.Mappings
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
            
            CreateMap<CallbackQuery, SettingsBackCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.Message.MessageId));

            CreateMap<CallbackQuery, SettingsDeparturesCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.Message.MessageId));

            CreateMap<CallbackQuery, SettingsCountriesCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.Message.MessageId));

            CreateMap<CallbackQuery, SettingsCountriesSubscribeCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.Message.MessageId))
                .ForMember(c => c.CountryId, exp => exp.MapFrom(f => ExtractCallbackQueryId(f.Data)));

            CreateMap<CallbackQuery, SettingsCountriesUnsubscribeCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.Message.MessageId))
                .ForMember(c => c.CountryId, exp => exp.MapFrom(f => ExtractCallbackQueryId(f.Data)));

            CreateMap<CallbackQuery, SettingsDeparturesSubscribeCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.Message.MessageId))
                .ForMember(c => c.DepartureId, exp => exp.MapFrom(f => ExtractCallbackQueryId(f.Data)));

            CreateMap<CallbackQuery, SettingsDeparturesUnsubscribeCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.Message.MessageId))
                .ForMember(c => c.DepartureId, exp => exp.MapFrom(f => ExtractCallbackQueryId(f.Data)));

            CreateMap<CallbackQuery, SettingsSubscribeCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.Message.MessageId));

            CreateMap<CallbackQuery, SettingsUnsubscribeCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.Message.MessageId));
        }

        private static int ExtractCallbackQueryId(string data)
        {
            var idStr = data.Split(':')[1];
            return int.Parse(idStr);
        }
    }
}