using BetterTravel.Domain.Enums;
using CSharpFunctionalExtensions;

namespace BetterTravel.Domain.ValueObjects
{
    public class ChatInfo : ValueObject<ChatInfo>
    {
        protected ChatInfo()
        {
        }
        
        private ChatInfo(string title, string description, ChatType type)
        {
            Title = title;
            Description = description;
            Type = type;
        }

        public string Title { get; }
        public string Description { get; }
        public ChatType Type { get; }

        public static Result<ChatInfo> Create(string title, string description, ChatType type)
        {
            var chat = new ChatInfo(title, description, type);
            return Result.Success(chat);
        }

        protected override bool EqualsCore(ChatInfo other) => 
            Title == other.Title &&
            Description == other.Description &&
            Type == other.Type;

        protected override int GetHashCodeCore() => 
            Title.GetHashCode() + 
            Description.GetHashCode() + 
            Type.GetHashCode();
    }
}