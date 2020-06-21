using System.Collections.Generic;

namespace BetterTravel.DataAccess.Abstraction.Entities
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LanguageCode { get; set; }
        public bool IsBot { get; set; }

        public virtual ICollection<Chat> Chat { get; set; }
    }
}