namespace BusinessLogic.Contracts
{
    public interface IEmailService
    {
        void SendVerificationEmail(string email);
        void SendEmail(string email, string subject, string body);
    }
}
