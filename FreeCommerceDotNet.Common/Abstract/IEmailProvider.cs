namespace FreeCommerceDotNet.Common.Abstract
{
    public interface IEmailProvider
    {
        bool Send(string from, string to, string message);
        bool Send(string to, string message);
    }
}