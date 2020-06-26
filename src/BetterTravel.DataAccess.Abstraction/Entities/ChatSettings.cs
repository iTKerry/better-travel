using BetterTravel.DataAccess.Abstraction.Entities.Base;
using CSharpFunctionalExtensions;

namespace BetterTravel.DataAccess.Abstraction.Entities
{
    public class ChatSettings : Entity
    {
        protected ChatSettings()
        {
        }
        
        private ChatSettings(bool isSubscribed)
        {
            IsSubscribed = isSubscribed;
        }

        public bool IsSubscribed { get; private set; }
        public int SettingsOfChatId { get; private set; }
        public virtual Chat Chat { get; private set; }

        public static Result<ChatSettings> Create(bool isSubscribed)
        {
            var chatSettings = new ChatSettings(isSubscribed);
            return Result.Ok(chatSettings);
        }

        public Result Subscribe()
        {
            if (IsSubscribed)
                return Result.Failure("You already subscribed.");
            
            IsSubscribed = true;
            
            return Result.Ok();
        }
        
        public Result Unsubscribe()
        {
            if (!IsSubscribed)
                return Result.Failure("You are not subscribed anyway.");
            
            IsSubscribed = false;
            
            return Result.Ok();
        }
    }
}