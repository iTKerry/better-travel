using System.Collections.Generic;
using BetterTravel.DataAccess.Abstraction.Entities.Base;
using BetterTravel.DataAccess.Abstraction.Entities.Enums;

namespace BetterTravel.DataAccess.Abstraction.Entities
{
    public class Chat : Entity
    {
        public long ChatId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ChatType ChatType { get; set; }
        public bool IsSubscribed { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}