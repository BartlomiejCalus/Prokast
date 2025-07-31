using AutoMapper;
using Prokast.Server.Entities;
using Prokast.Server.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Prokast.Server.Models;
using System.Web.Http;
using Prokast.Server.Models.ResponseModels;
using Org.BouncyCastle.Asn1.Cms;
using AutoMapper.Execution;
//using System.Net.Mail;

namespace Prokast.Server.Services
{
    public class MailingService: IMailingService
    {
        private readonly SmtpSettings _smtpSettings;
        Random random = new Random();

        public MailingService(ProkastServerDbContext dbContext, IMapper mapper, IOptions<SmtpSettings> smtpOptions)
        {
            _smtpSettings = smtpOptions.Value;
        }

        public void SendEmail([FromBody] EmailMessage emailMessage)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Prokast", _smtpSettings.Email));
            
            foreach(var recipient in emailMessage.To)
            {
                message.To.Add(MailboxAddress.Parse(recipient));
            }

            message.Subject = emailMessage.Subject;

            //test wysyłania załączników
            /*string txt = "R_kolokwium.txt";
            var fileContent = System.Text.Encoding.UTF8.GetBytes("Ala ma kota 123.");
            emailMessage.Attachments.Add((txt, fileContent));*/


            var bodybuilder = new BodyBuilder()
            {
                TextBody = emailMessage.Body
            };

            if (emailMessage.Attachments != null)
            {
                foreach (var attachment in emailMessage.Attachments)
                {
                    if (attachment.Item1 != null && attachment.Item2 != null)
                    {
                        var mimeType = MimeKit.MimeTypes.GetMimeType(attachment.Item1);
                        var typeparts = mimeType.Split('/');
                        var contentType = new ContentType(typeparts[0], typeparts[1]);

                        bodybuilder.Attachments.Add(attachment.Item1, attachment.Item2, contentType);
                    }

                }
            }

            message.Body = bodybuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                var secureOption = _smtpSettings.UseSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls;

                client.Connect(_smtpSettings.Server, _smtpSettings.Port, secureOption);

                client.Authenticate(_smtpSettings.Email, _smtpSettings.Password);

                client.Send(message);

                client.Disconnect(true);
            }
        }
    }
}
