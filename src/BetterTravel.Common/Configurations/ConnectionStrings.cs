namespace BetterTravel.Common.Configurations
{
    public class ConnectionStrings
    {
        public const string Key = nameof(ConnectionStrings);
        
        public string BetterTravelDb { get; set; }
        public string BetterTravelCache { get; set; }
    }
}