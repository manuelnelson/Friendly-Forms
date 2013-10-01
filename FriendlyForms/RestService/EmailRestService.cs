using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using FriendlyForms.Helpers;
using Models.ViewModels;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;

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

        [Route("/Emails/Survey", "POST")]
        public class SurveyDto : IReturn<EmailDto>
        {
            public int LoginOne { get; set; }
            public long UserId { get; set; }
            public string LoginOneComment { get; set; }
            public int LoginTwo { get; set; }
            public string LoginTwoComment { get; set; }
            public int LoginThree { get; set; }
            public string LoginThreeComment { get; set; }
            public int DataEntryOne { get; set; }
            public string DataEntryOneComment { get; set; }
            public int DataEntryTwo { get; set; }
            public string DataEntryTwoComment { get; set; }
            public int DataEntryThree { get; set; }
            public string DataEntryThreeComment { get; set; }
            public int DataEntryFour { get; set; }
            public string DataEntryFourComment { get; set; }
            public int DataEntryFive { get; set; }
            public string DataEntryFiveComment { get; set; }
            public int NavigationOne { get; set; }
            public string NavigationOneComment { get; set; }
            public int NavigationTwo { get; set; }
            public string NavigationTwoComment { get; set; }
            public int NavigationThree { get; set; }
            public string NavigationThreeComment { get; set; }
            public int NavigationFour { get; set; }
            public string NavigationFourComment { get; set; }
            public int OutputOne { get; set; }
            public string OutputOneComment { get; set; }
            public int OutputTwo { get; set; }
            public string OutputTwoComment { get; set; }
            public int OutputThree { get; set; }
            public string OutputThreeComment { get; set; }
            public int OutputFour { get; set; }
            public string OutputFourComment { get; set; }
            public string FinalComment { get; set; }
        }

        public class EmailsService : ServiceBase
        {
            public IEmailService EmailService { get; set; } //Injected by IOC
            public IUserAuthRepository UserAuthRepository { get; set; } //Injected by IOC

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
            public void Post(SurveyDto request)
            {
                var userAuth = UserAuthRepository.GetUserAuth(UserSession.UserAuthId);
                var message = "Survey Results from " + userAuth.DisplayName + "<br/>";
                message += "Login Question 1: " + Enum.GetName(typeof (Survey), request.LoginOne) + "<br/>";
                message += "Login Question 1 Comment: " + request.LoginOneComment + "<br/>";
                message += "Login Question 2: " + Enum.GetName(typeof(Survey), request.LoginTwo) + "<br/>";
                message += "Login Question 2 Comment: " + request.LoginTwoComment + "<br/>";
                message += "Login Question 3: " + Enum.GetName(typeof(Survey), request.LoginThree) + "<br/>";
                message += "Login Question 3 Comment: " + request.LoginThreeComment + "<br/>";
                message += "Data Entry Question 1: " + Enum.GetName(typeof(Survey), request.DataEntryOne) + "<br/>";
                message += "Data Entry Question 1 Comment: " + request.DataEntryOneComment + "<br/>";
                message += "Data Entry Question 2: " + Enum.GetName(typeof(Survey), request.DataEntryTwo) + "<br/>";
                message += "Data Entry Question 2 Comment: " + request.DataEntryTwoComment + "<br/>";
                message += "Data Entry Question 3: " + Enum.GetName(typeof(Survey), request.DataEntryThree) + "<br/>";
                message += "Data Entry Question 3 Comment: " + request.DataEntryThreeComment + "<br/>";
                message += "Data Entry Question 4: " + Enum.GetName(typeof(Survey), request.DataEntryFour) + "<br/>";
                message += "Data Entry Question 4 Comment: " + request.DataEntryFourComment + "<br/>";
                message += "Data Entry Question 5: " + Enum.GetName(typeof(Survey), request.DataEntryFive) + "<br/>";
                message += "Data Entry Question 5 Comment: " + request.DataEntryFiveComment + "<br/>";
                message += "Navagation Question 1: " + Enum.GetName(typeof(Survey), request.NavigationOne) + "<br/>";
                message += "Navagation Question 1 Comment: " + request.NavigationOneComment + "<br/>";
                message += "Navagation Question 2: " + Enum.GetName(typeof(Survey), request.NavigationTwo) + "<br/>";
                message += "Navagation Question 2 Comment: " + request.NavigationTwoComment + "<br/>";
                message += "Navagation Question 3: " + Enum.GetName(typeof(Survey), request.NavigationThree) + "<br/>";
                message += "Navagation Question 3 Comment: " + request.NavigationThreeComment + "<br/>";
                message += "Navagation Question 4: " + Enum.GetName(typeof(Survey), request.NavigationFour) + "<br/>";
                message += "Navagation Question 4 Comment: " + request.NavigationFourComment + "<br/>";
                message += "Output Question 1: " + Enum.GetName(typeof(Survey), request.OutputOne) + "<br/>";
                message += "Output Question 1 Comment: " + request.OutputOneComment + "<br/>";
                message += "Output Question 2: " + Enum.GetName(typeof(Survey), request.OutputTwo) + "<br/>";
                message += "Output Question 2 Comment: " + request.OutputTwoComment + "<br/>";
                message += "Output Question 3: " + Enum.GetName(typeof(Survey), request.OutputThree) + "<br/>";
                message += "Output Question 3 Comment: " + request.OutputThreeComment + "<br/>";
                message += "Output Question 4: " + Enum.GetName(typeof(Survey), request.OutputFour) + "<br/>";
                message += "Output Question 4 Comment: " + request.OutputFourComment + "<br/>";
                message += "Final Comment: " + request.FinalComment + "<br/>";

                var toEmails = new List<string>
                    {
                        "gcalhoun@splitsolutions.com",
                        "enelson@splitsolutions.com",
                    };
                EmailService.SendEmail(toEmails, "Split solution", message);
            }

        }

    }

}
