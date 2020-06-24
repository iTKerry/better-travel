using BetterTravel.DataAccess.Abstraction.Entities.Base;
using BetterTravel.DataAccess.Abstraction.Entities.Enums;
using BetterTravel.DataAccess.Abstraction.ValueObjects;
using CSharpFunctionalExtensions;

namespace BetterTravel.DataAccess.Abstraction.Entities
{
    public class Chat : AggregateRoot
    {
        protected Chat()
        {
        }
        
        private Chat(long chatId, ChatInfo info, bool isSubscribed)
        {
            ChatId = chatId;
            Info = info;
            IsSubscribed = isSubscribed;
        }

        public long ChatId { get; }
        public ChatInfo Info { get; private set; }
        public bool IsSubscribed { get; private set; }

        public static Result<Chat> Create(long chatId, ChatInfo info, bool subscribe)
        {
             if (info is null)
                 Result.Failure<Chat>("Chat information not provided.");
             
             var chat = new Chat(chatId, info, subscribe);
             
             return Result.Ok(chat);
        }

        public Result Subscribe()
        {
            if (IsSubscribed)
                return Result.Failure("You already subscribed.");
            IsSubscribed = true;
            return Result.Success();
        }

        public Result Unsubscribe()
        {
            if (!IsSubscribed)
                return Result.Failure("You already unsubscribed.");
            IsSubscribed = false;
            return Result.Success();
        }

        public Result UpdateInfo(string title, string description, ChatType type)
        {
            var chatInfoResult = ChatInfo.Create(title, description, type);
            if (chatInfoResult.IsFailure)
                return Result.Failure(chatInfoResult.Error);

            Info = chatInfoResult.Value;

            return Result.Success();
        }
    }
}