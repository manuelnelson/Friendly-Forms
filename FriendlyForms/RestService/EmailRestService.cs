using BusinessLogic.Contracts;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    public class EmailRestService
    {
        [Route("/Emails", "POST")]
        public class EmailDto : IReturn<EmailDto>
        {
            public long UserId { get; set; }
            public string Email { get; set; }
            public string Message { get; set; }
            public string LawFirm { get; set; }
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
                EmailService.SendEmail(request.Email, "Split solutions Account Creation Request", message); 
            }

        }

    }

}
