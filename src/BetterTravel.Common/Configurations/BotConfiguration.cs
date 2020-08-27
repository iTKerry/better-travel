namespace BetterTravel.Common.Configurations
{
    public class BotConfiguration
    {
        public const string Key = nameof(BotConfiguration);
        
        public string WebhookUrl { get; set; }
        public string BotToken { get; set; }

        public override string ToString() => $"{WebhookUrl}/{BotToken}";
    }
}