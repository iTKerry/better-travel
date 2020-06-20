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
                .ForMember(s => s.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id))
                .ForMember(s => s.IsBot, exp => exp.MapFrom(f => f.Message.From.IsBot));
            
            CreateMap<Update, StatusCommand>()
                .ForMember(s => s.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id));
            
            CreateMap<Update, SubscribeCommand>()
                .ForMember(s => s.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id))
                .ForMember(s => s.IsBot, exp => exp.MapFrom(f => f.Message.From.IsBot));

            CreateMap<Update, UnsubscribeCommand>()
                .ForMember(s => s.ChatId, exp => exp.MapFrom(f => f.Message.Chat.Id));
        }
    }
}