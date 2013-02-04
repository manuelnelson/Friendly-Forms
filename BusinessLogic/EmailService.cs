using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using BusinessLogic.Contracts;
using BusinessLogic.Properties;
using Elmah;

namespace BusinessLogic
{
    public class EmailService : IEmailService
    {
        private readonly string _fromEmail;
        private readonly string _fromPassword;
        private readonly string _mailServerName;
        public EmailService()
        {
            _fromEmail = Resources.FromEmail;
            _fromPassword = Resources.FromPassword;
            _mailServerName = Resources.MailServerName;
        }

        public void SendVerificationEmail(string email, string hashedEmail)
        {
            const string subject = "Thanks for signing up to Split Solutions!";
            var message = "First, we need to verify your account.  To verify your account, please click on the following link:<br><br>";
            message += "<a href='http://www.splitsolutions.com/Account/Verify?email=" + email + "&randomhash=" + hashedEmail + "' target='_blank' title='Verification'>Verify</a> <br><br>";
            message += "We hope the application is as easy to use as possible!<br><br> -The Split Solutions Team";
            SendEmail(email, subject, message);
        }

        public void SendEmail(string email, string subject, string body)
        {
            var message = new MailMessage()
            {
                From = new MailAddress(_fromEmail),
                Subject = subject,
                Body = body,
                SubjectEncoding = Encoding.UTF8,
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(email));

            var mine = new System.Net.NetworkCredential(_fromEmail, _fromPassword);
            var mailClient = new SmtpClient
            {
                UseDefaultCredentials = false,
                Credentials = mine,
                Host = _mailServerName,
                Port = 587,
                EnableSsl = true
            };
            try
            {
                mailClient.Send(message);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new BusinessServicesException("Unable to send E-mail", ex);
            }
            message.Dispose();
        }
    }
}
