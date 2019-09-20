using System;

namespace FreeCommerceDotNet.Models.Util
{
    public class LogEntry
    {
        public readonly LogType Severity;
        public readonly string Message;
        public readonly string Owner;

        public LogEntry(LogType severity, string message,string owner)
        {
            if (message == null) throw new ArgumentNullException("message");
            if (owner == null) throw new ArgumentNullException("owner");
            if (message == string.Empty) throw new ArgumentException("empty", "message");
            if (owner == string.Empty) throw new ArgumentException("empty", "owner");
            this.Severity = severity;
            this.Message = message;
            this.Owner = owner;
        }
    
    }
}