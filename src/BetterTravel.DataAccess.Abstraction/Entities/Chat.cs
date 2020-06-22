using BetterTravel.DataAccess.Abstraction.Entities.Base;
using BetterTravel.DataAccess.Abstraction.ValueObjects;

namespace BetterTravel.DataAccess.Abstraction.Entities
{
    public class Chat : Entity
    {
        protected Chat()
        {
        }
        
        public Chat(long chatId, ChatInfo info, bool isSubscribed)
        {
            ChatId = chatId;
            Info = info;
            IsSubscribed = isSubscribed;
        }

        public long ChatId { get; }
        public ChatInfo Info { get; }
        public bool IsSubscribed { get; }
    }
}