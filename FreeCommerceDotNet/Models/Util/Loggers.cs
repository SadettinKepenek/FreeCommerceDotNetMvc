using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.Util
{
    public class DatabaseLogger : ILogger
    {
        public void Log(LogEntry entry)
        {
            LoggerExtensions.Log(this, "Database Logger "+entry.Message, entry.Owner);
        }
    }
    public class EMailLogger : ILogger
    {
        public void Log(LogEntry entry)
        {
            LoggerExtensions.Log(this, "Email Logger "+entry.Message, entry.Owner);
        }
    }
}