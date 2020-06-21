using System.Collections.Generic;
using BetterTravel.DataAccess.Abstraction.Entities.Base;
using BetterTravel.DataAccess.Abstraction.ValueObjects;

namespace BetterTravel.DataAccess.Abstraction.Entities
{
    public class User : Entity
    {
        public string Username { get; set; }
        public Name Name { get; set; }
        public string LanguageCode { get; set; }
        public bool IsBot { get; set; }

        public virtual ICollection<Chat> Chat { get; set; }
    }
}