using MimeKit;
using System.IO;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using CoreApiWeaponRegister.Entities.Models.Correo;
using CoreApiWeaponRegister.Repository.Interfaces;
using CoreApiWeaponRegister.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Repository
{
    public class MailService : IMailService
    {
        //private readonly MailSettings _mailSettings;

        MailSettings _emailSettings = null;
        //public MailService(IOptions<MailSettings> mailSettings)
        public MailService(IOptions<MailSettings> options)
        {
            //_mailSettings = mailSettings.Value;
            _emailSettings = options.Value;
        }

        //public async Task SendEmailAsync(MailRequest mailRequest)
        //{
        //    var email = new MimeMessage();
        //    email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
        //    email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        //    email.Subject = mailRequest.Subject;
        //    var builder = new BodyBuilder();
        //    if (mailRequest.Attachments != null)
        //    {
        //        byte[] fileBytes;
        //        foreach (var file in mailRequest.Attachments)
        //        {
        //            if (file.Length > 0)
        //            {
        //                using (var ms = new MemoryStream())
        //                {
        //                    file.CopyTo(ms);
        //                    fileBytes = ms.ToArray();
        //                }
        //                builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
        //            }
        //        }
        //    }
        //    builder.HtmlBody = mailRequest.Body;
        //    email.Body = builder.ToMessageBody();
        //    using var smtp = new SmtpClient();
        //    smtp.Connect(_mailSettings.Host, _mailSettings.Port, true);//SecureSocketOptions.StartTls);
        //    smtp.AuthenticationMechanisms.Remove("XOAUTH2");
        //    smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        //    await smtp.SendAsync(email);
        //    smtp.Disconnect(true);
        //}

        //public  MailRequest SendEmailAsyncV2(MailRequest mailRequest)
        //{
        //    var email = new MimeMessage();
        //    email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
        //    email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        //    email.Subject = mailRequest.Subject;
        //    var builder = new BodyBuilder();
        //    if (mailRequest.Attachments != null)
        //    {
        //        byte[] fileBytes;
        //        foreach (var file in mailRequest.Attachments)
        //        {
        //            if (file.Length > 0)
        //            {
        //                using (var ms = new MemoryStream())
        //                {
        //                    file.CopyTo(ms);
        //                    fileBytes = ms.ToArray();
        //                }
        //                builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
        //            }
        //        }
        //    }
        //    builder.HtmlBody = mailRequest.Body;
        //    email.Body = builder.ToMessageBody();
        //    using var smtp = new SmtpClient();
        //    smtp.Connect(_mailSettings.Host, _mailSettings.Port, true);//SecureSocketOptions.StartTls);
        //    smtp.AuthenticationMechanisms.Remove("XOAUTH2");
        //    smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        //    smtp.SendAsync(email);
        //    smtp.Disconnect(true);

        //    return mailRequest;
        //}

        public bool SendEmail(EmailData emailData)
        {
            try
            {
                MimeMessage emailMessage = new MimeMessage();
                MailboxAddress emailFrom = new MailboxAddress(_emailSettings.DisplayName, _emailSettings.Mail);
                emailMessage.From.Add(emailFrom);
                MailboxAddress emailTo = new MailboxAddress(emailData.EmailToName, emailData.EmailToId);
                emailMessage.To.Add(emailTo);
                emailMessage.Subject = emailData.EmailSubject;
                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.TextBody = emailData.EmailBody;
                emailMessage.Body = emailBodyBuilder.ToMessageBody();
                SmtpClient emailClient = new SmtpClient();
                emailClient.Connect(_emailSettings.Host, _emailSettings.Port, true);
                emailClient.Authenticate(_emailSettings.Mail, _emailSettings.Password);
                emailClient.Send(emailMessage);
                emailClient.Disconnect(true);
                emailClient.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                //Log Exception Details
                return false;
            }
        }
    }
}
