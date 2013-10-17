using System.Configuration;
using System.Net;
using BusinessLogic.Contracts;
using FriendlyForms.App_Start;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.Auth;
using System;
using System.Collections.Generic;

namespace FriendlyForms.RestService
{
    [Route("/passwordreset")]
    [Route("/api/auth/passwordreset")]
    public class PasswordResetRequest
    {
        public string Email { get; set; }

        public string Id { get; set; }

        public string NewPassword { get; set; }
    }

    public class PasswordResetResponse
    {
        public string Id { get; set; }

        public bool PasswordChanged { get; set; }
    }

    [DefaultRequest(typeof(PasswordResetRequest))]
    internal class PasswordResetService : ServiceBase
    {
        private IUserAuthRepository UserAuthRepo { get; set; }
        private IEmailService EmailService { get; set; }
        //Called when a password reset link is clicked.
        public object Get(PasswordResetRequest request)
        {
            //if (!ValidationLibrary.validate(request.Id, ValidationLibrary.GUID))
            //    return new PasswordResetResponse() { valid = false };
            //Display Change Password Screen
            var resetrequest = Cache.Get<PasswordResetRequest>(request.Id);
            var response = new PasswordResetResponse { Id = resetrequest.Id };
            return response;
        }

        //Called when the password request is initiated.
        public string Post(PasswordResetRequest request)
        {
            if (request.Email == null)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "You must provide an email address.");
            }

            if (UserAuthRepo.GetUserAuthByUserName(request.Email) == null)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "Email Address not registered.");
            }

            request.Id = Guid.NewGuid().ToString();
            Cache.Add<PasswordResetRequest>(request.Id, request, new TimeSpan(1, 0, 0));
            EmailService.SendEmail(new List<string>(){request.Email},"Password Reset", "<p>Uh Oh!  Have you forgotten your password?</p><p>Somebody asked for a password reminder, if it was you click <a href=\"" + ConfigurationManager.AppSettings["FullDomain"] + "api/passwordreset?id=" + request.Id + "\">this link to reset it now.</a></p><p>This link is valid for 1 hour</p><p>If you remember your password, or you didn't request a reset, you can ignore this email</p><p>Regards</p><p>The Split Solutions Team</p>");

            return "An email has been sent with a link to reset your password.";
        }

        public PasswordResetResponse Put(PasswordResetRequest request)
        {
            //if (!ValidationLibrary.validate(request.Id, ValidationLibrary.GUID))
            //    return new PasswordResetResponse() { valid = false };

            //if (!ValidationLibrary.validate(request.Newpassword, ValidationLibrary.UserPassword))
            //    return new PasswordResetResponse() { valid = false };

            //Changes the password
            var resetrequest = Cache.Get<PasswordResetRequest>(request.Id);

            var response = new PasswordResetResponse();
            if (resetrequest == null)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "The key for this password change has expired.");
            }

            var existingUser = UserAuthRepo.GetUserAuthByUserName(resetrequest.Email);
            if (existingUser == null)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "Email doesn't exist for this user.");
            }

            UserAuthRepo.UpdateUserAuth(existingUser, existingUser, request.NewPassword);

            response.PasswordChanged = true;
            Cache.Remove(resetrequest.Id);
            return response;
        }
    }
}