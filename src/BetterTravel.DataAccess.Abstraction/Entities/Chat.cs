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
        
        private Chat(long chatId, ChatInfo info, ChatSettings settings)
        {
            ChatId = chatId;
            Info = info;
            Settings = settings;
        }

        public long ChatId { get; private set; }
        public virtual ChatSettings Settings { get; private set; }
        public virtual ChatInfo Info { get; private set; }

        public static Result<Chat> Create(long chatId, ChatInfo info, ChatSettings settings)
        {
             if (info is null)
                 Result.Failure<Chat>("Chat information not provided.");
             if (settings is null)
                 Result.Failure<Chat>("Settings information not provided.");
             
             var chat = new Chat(chatId, info, settings);
             
             return Result.Ok(chat);
        }

        public Result Subscribe() =>
            Settings.Subscribe();

        public Result Unsubscribe() =>
            Settings.Unsubscribe();

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