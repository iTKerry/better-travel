using System.Collections.Generic;
using BetterTravel.Domain.Enums;

namespace BetterTravel.Domain
{
    public class Chat
    {
        public long ChatId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ChatType ChatType { get; set; }

        public List<User> User { get; set; }
    }
}