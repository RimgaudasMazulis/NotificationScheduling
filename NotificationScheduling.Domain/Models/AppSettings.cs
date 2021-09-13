namespace NotificationScheduling.Domain.Models
{
    public class AppSettings
    {
        public static string ConnectionString { get; private set; }
        public static int CacheTimeoutInMinutes { get; private set; }
    }
}
