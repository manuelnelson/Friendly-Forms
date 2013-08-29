using System.Collections.Generic;

namespace BusinessLogic.Contracts
{
    public interface IEmailService
    {
        void SendVerificationEmail(string email);
        void SendEmail(List<string> toEmails, string subject, string body);
    }
}
