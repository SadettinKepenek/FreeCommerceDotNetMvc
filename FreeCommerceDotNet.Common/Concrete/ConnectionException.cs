using System;
using System.Collections;

namespace FreeCommerceDotNet.Common.Concrete
{
    [Serializable]
    public class ConnectionException:Exception
    {
        public override string Message { get; }
        public override IDictionary Data { get; }
        public override string StackTrace { get; }
        public override string HelpLink { get; set; }
        public override string Source { get; set; }

        public ConnectionException()
        {
            
        }

        public ConnectionException(string message):base(String.Format("Connection Can't Open {0}",message))
        {
            Message = message;
            
        }
    }
}