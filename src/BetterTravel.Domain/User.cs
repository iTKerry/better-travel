using System.Collections.Generic;

namespace BetterTravel.Domain
{
    public class User
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LanguageCode { get; set; }
        public bool IsBot { get; set; }

        public List<Chat> Chat { get; set; }
    }
}