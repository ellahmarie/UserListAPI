using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersDetailAPI.IRepository;
using UsersDetailAPI.Models;

namespace UsersDetailAPI.Repository
{
    public class SendEmailRepository : IEmailRepository
    {
        private readonly SmtpSettings _setting;
        private readonly IWebHostEnvironment _env;

        public SendEmailRepository( IOptions<SmtpSettings> setting, IWebHostEnvironment env)
        {
            _setting = setting.Value;
            _env = env;
        }

        public async Task SendEmail(string email, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_setting.SenderName, _setting.SenderEmail));
            message.To.Add(new MailboxAddress(email));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(_setting.Server, _setting.Port, false);

                await client.AuthenticateAsync(_setting.Username, _setting.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

        }
    }
}
