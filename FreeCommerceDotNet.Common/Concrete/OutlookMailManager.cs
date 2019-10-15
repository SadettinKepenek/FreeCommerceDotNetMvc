using System;
using System.Net;
using System.Net.Mail;
using FreeCommerceDotNet.Common.Abstract;

namespace FreeCommerceDotNet.Common.Concrete
{
    public class OutlookMailManager:IEmailProvider
    {
        private string SmtpUsername { get; }
        private string SmtpPassword { get; }
        private string Host { get; }

        public OutlookMailManager()
        {
            this.SmtpUsername = "freecommercedotnet@hotmail.com";
            this.SmtpPassword = "freecommerce123"; 
        }
        public OutlookMailManager(string smtpUsername, string smtpPassword, string host)
        {
            if (string.IsNullOrEmpty(smtpUsername) || string.IsNullOrEmpty(smtpPassword)||string.IsNullOrEmpty(host))
            {
                throw new ArgumentException("Parametreler eksik gönderildi");
            }
            this.SmtpUsername = smtpUsername;
            this.SmtpPassword = smtpPassword;
            this.Host = host;
        }
        public bool Send(string to, string subject, string message)
        {
            SmtpClient client = new SmtpClient(host:Host);
            //If you need to authenticate
            client.Credentials = new NetworkCredential(SmtpUsername, SmtpPassword);
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(SmtpUsername);
            mailMessage.To.Add(to);
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;
            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception e)
            {

                throw new Exception("Mail Send Failed " + e.StackTrace);
            }

        }
    }
}