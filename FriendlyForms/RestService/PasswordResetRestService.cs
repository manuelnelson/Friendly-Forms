using System.Configuration;
using System.Net;
using BusinessLogic.Contracts;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.Auth;
using System;
using System.Collections.Generic;

namespace FriendlyForms.RestService
{
    [Route("/passwordreset")]
    public class PasswordResetRequest
    {
        public string Email { get; set; }

        public string Id { get; set; }

        public string Password { get; set; }
    }

    public class PasswordResetResponse
    {
        public string Id { get; set; }

        public bool PasswordChanged { get; set; }
    }

    public class PasswordResetService : ServiceBase
    {
        public IUserAuthRepository UserAuthRepository { get; set; }
        public IEmailService EmailService { get; set; }
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
        public object Put(PasswordResetRequest request)
        {
            //if (!ValidationLibrary.validate(request.Id, ValidationLibrary.GUID))
            //    return new PasswordResetResponse() { valid = false };

            //if (!ValidationLibrary.validate(request.Newpassword, ValidationLibrary.UserPassword))
            //    return new PasswordResetResponse() { valid = false };

            //Changes the password
            var resetrequest = Cache.Get<PasswordResetRequest>(request.Id);

            if (resetrequest == null)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "The key for this password change has expired.");
            }

            var existingUser = UserAuthRepository.GetUserAuthByUserName(resetrequest.Email);
            if (existingUser == null)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "Email doesn't exist for this user.");
            }

            UserAuthRepository.UpdateUserAuth(existingUser, existingUser, request.Password);
            Cache.Remove(resetrequest.Id);

            return new PasswordResetRequest
            {
                Email = existingUser.Email,
            };
        }

        //Called when the password request is initiated.
        public object Post(PasswordResetRequest request)
        {
            if (request.Email == null)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "You must provide an email address.");
            }
            try
            {
                var test = UserAuthRepository.GetUserAuthByUserName(request.Email);
            }
            catch (Exception ex)
            {
                throw new HttpError(HttpStatusCode.BadRequest, "Email Address not registered.");                
            }

            request.Id = Guid.NewGuid().ToString();
            Cache.Add<PasswordResetRequest>(request.Id, request, new TimeSpan(1, 0, 0));
            EmailService.SendEmail(new List<string>(){request.Email},"Password Reset", "<p>We have received word that you may have forgotten your password.  If it was you, click <a href=\"" + ConfigurationManager.AppSettings["FullDomain"] + "#/Account/PasswordReset?id=" + request.Id + "\">this link to reset it now.</a> This link is valid for one hour.</p><p>If you remember your password, or you didn't request a reset, you can ignore this email</p><p>Thanks!</p><p>The Split Solutions Team</p>");

            return "An email has been sent with a link to reset your password.";
        }

    }
}