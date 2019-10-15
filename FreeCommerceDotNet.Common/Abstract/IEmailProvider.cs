namespace FreeCommerceDotNet.Common.Abstract
{
    public interface IEmailProvider
    {

        bool Send(string to,string subject, string message);
    }
}