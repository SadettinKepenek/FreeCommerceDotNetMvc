using System;

namespace FreeCommerceDotNet.Common.Abstract
{
    public interface ILogger
    {
        void Log(string message);
        void Log(string message, string owner);
        void Log(string message, string owner,Exception e);

    }
}