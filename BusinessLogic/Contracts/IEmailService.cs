namespace BusinessLogic.Contracts
{
    public interface IEmailService
    {
        void SendVerificationEmail(string email, string hashedEmail);
        void SendEmail(string email, string subject, string body);
    }
}
