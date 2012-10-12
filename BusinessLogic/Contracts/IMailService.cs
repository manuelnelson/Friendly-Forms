using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Contracts
{
    public interface IMailService
    {
        void SendVerificationEmail(string email, string hashedEmail);
        void SendEmail(string email, string subject, string body);
    }
}
