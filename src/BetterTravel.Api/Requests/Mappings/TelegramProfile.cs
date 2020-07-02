using AutoMapper;
using BetterTravel.Commands.Telegram.Settings;
using BetterTravel.Commands.Telegram.SettingsBack;
using BetterTravel.Commands.Telegram.SettingsCountries;
using BetterTravel.Commands.Telegram.SettingsDepartures;
using BetterTravel.Commands.Telegram.SettingsSubscribe;
using BetterTravel.Commands.Telegram.SettingsUnsubscribe;
using BetterTravel.Commands.Telegram.Start;
using BetterTravel.Commands.Telegram.Status;
using Telegram.Bot.Types;

namespace BetterTravel.Api.Requests.Mappings
{
    public class TelegramProfile : Profile
    {
        public TelegramProfile()
        {
            CreateMap<Update, StartCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id))
                .ForMember(c => c.IsBot, exp => exp.MapFrom(f => f.Message.From.IsBot))
                .ForMember(c => c.Type, exp => exp.MapFrom(f => f.Message.Chat.Type))
                .ForMember(c => c.Title, exp => exp.MapFrom(f => f.Message.Chat.Title))
                .ForMember(c => c.Description, exp => exp.MapFrom(f => f.Message.Chat.Description));
            
            CreateMap<Update, StatusCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id));
            
            CreateMap<Update, SettingsBackCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.CallbackQuery.From.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.CallbackQuery.Message.MessageId));

            CreateMap<Update, SettingsDeparturesCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.CallbackQuery.From.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.CallbackQuery.Message.MessageId));

            CreateMap<Update, SettingsCountriesCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.CallbackQuery.From.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.CallbackQuery.Message.MessageId));

            CreateMap<Update, SettingsSubscribeCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.CallbackQuery.From.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.CallbackQuery.Message.MessageId));

            CreateMap<Update, SettingsUnsubscribeCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.CallbackQuery.From.Id))
                .ForMember(c => c.MessageId, exp => exp.MapFrom(f => f.CallbackQuery.Message.MessageId));
            
            CreateMap<Update, SettingsCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id));
        }
    }
}