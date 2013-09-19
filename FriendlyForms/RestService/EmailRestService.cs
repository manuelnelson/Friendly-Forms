using System.Collections.Generic;
using BusinessLogic.Contracts;
using FriendlyForms.Helpers;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    public class EmailRestService
    {
        [Route("/Emails", "POST")]
        public class EmailDto : IReturn<EmailDto>, IHasUser
        {
            public long UserId { get; set; }
            public string Email { get; set; }
            public string Message { get; set; }
            public string LawFirm { get; set; }
        }

        [Route("/Emails/Feedback", "POST")]
        public class FeedbackDto : IReturn<EmailDto>
        {
            public string Email { get; set; }
            public string Message { get; set; }
            public string Path { get; set; }
        }

        public class EmailsService : Service
        {
            public IEmailService EmailService { get; set; } //Injected by IOC

            public void Post(EmailDto request)
            {
                var link = "http://splitsolutions.com/Account/LogonFirm?ReferralId=" + request.UserId + "&LawFirm=" +
                           request.LawFirm;
                var message = request.Message.Replace("\r\n", "<br>");
                message = message +
                              "<br><br>To create an account, fill out the brief account firm that you'll find following this <a href='" +
                              link + "' title='Create Account' target='_blank'>link</a>.";

                EmailService.SendEmail(new List<string>
                    {
                       request.Email 
                    }, "Split solutions Account Creation Request", message); 
            }
            public void Post(FeedbackDto request)
            {
                var message = request.Message.Replace("\r\n", "<br>");
                message = "Feedback from " + request.Email + " sent from Path - " + request.Path + ": <br><br>" + message;
                var toEmails = new List<string>
                    {
                        "gcalhoun@splitsolutions.com",
                        "enelson@splitsolutions.com"
                    };
                EmailService.SendEmail(toEmails, "Split solutions Feedback", message);
            }

        }

    }

}
