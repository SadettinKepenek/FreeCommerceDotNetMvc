using System;
using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.Util
{
    public static class LoggerExtensions
    {
        public static void Log(this ILogger logger, string message,string owner)
        {
            logger.Log(new LogEntry(LogType.Information, message,owner));
        }

       
    }

    public enum LogType
    {
        Alert,
        Warning,
        Crash,
        Error,
        Information
    }
}