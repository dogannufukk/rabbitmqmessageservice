using MessageServiceExample.Core.Configuration.Abstract;
using MessageServiceExample.Core.Models.RequestModels;
using MessageServiceExample.Core.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MessageServiceExample.Core.Services.Concrete
{
    public class MessageService : IMessageService
    {
        IMailConfiguration _mailConfiguration;
        public MessageService(IMailConfiguration mailConfiguration)
        {
            _mailConfiguration = mailConfiguration;
        }
        public async Task<bool> SendMailAsync(SendMailModel model)
        {
            try
            {
                var smtpClient = SetSmtpSettings();
                MailMessage mailMessage = new();
                mailMessage.From = new MailAddress(_mailConfiguration.User);
                mailMessage.Subject = model.Title;
                mailMessage.Body = model.Content;
                mailMessage.To.Add(model.To);
                mailMessage.IsBodyHtml = true;
                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        SmtpClient SetSmtpSettings()
        {
            return new SmtpClient()
            {
                UseDefaultCredentials = false,
                EnableSsl = _mailConfiguration.SSL,
                Host = _mailConfiguration.Host,
                Port = _mailConfiguration.Port,
                Credentials = new NetworkCredential(_mailConfiguration.User, _mailConfiguration.Password)
            };
        }
    }
}
