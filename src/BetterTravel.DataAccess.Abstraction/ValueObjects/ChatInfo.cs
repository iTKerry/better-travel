using BetterTravel.DataAccess.Abstraction.Entities.Enums;
using BetterTravel.DataAccess.Abstraction.ValueObjects.Base;

namespace BetterTravel.DataAccess.Abstraction.ValueObjects
{
    public class ChatInfo : ValueObject<ChatInfo>
    {
        protected ChatInfo()
        {
        }
        
        public ChatInfo(string title, string description, ChatType type)
        {
            Title = title;
            Description = description;
            Type = type;
        }

        public string Title { get; }
        public string Description { get; }
        public ChatType Type { get; }
        
        protected override int GetHashCodeCore() => 
            Title.GetHashCode() + 
            Description.GetHashCode() + 
            Type.GetHashCode();

        protected override bool EqualCore(ChatInfo other) =>
            Title == other.Title &&
            Description == other.Description &&
            Type == other.Type;
    }
}