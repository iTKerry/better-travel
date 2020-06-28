using AutoMapper;
using BetterTravel.Commands.Telegram.Start;
using BetterTravel.Commands.Telegram.Status;
using BetterTravel.Commands.Telegram.Subscribe;
using BetterTravel.Commands.Telegram.Unsubscribe;
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
            
            CreateMap<Update, SubscribeCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id))
                .ForMember(c => c.IsBot, exp => exp.MapFrom(f => f.Message.From.IsBot))
                .ForMember(c => c.Type, exp => exp.MapFrom(f => f.Message.Chat.Type))
                .ForMember(c => c.Title, exp => exp.MapFrom(f => f.Message.Chat.Title))
                .ForMember(c => c.Description, exp => exp.MapFrom(f => f.Message.Chat.Description));

            CreateMap<Update, UnsubscribeCommand>()
                .ForMember(c => c.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id));
        }
    }
}