using System;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLogic.Contracts;
using BusinessLogic.Models;
using FriendlyForms.Authentication;
using FriendlyForms.Helpers;
using FriendlyForms.Models;
using Models.ViewModels;

namespace FriendlyForms.Controllers
{
    public class AccountController : Controller
    {
        private readonly IFormsAuthentication _formsAuthentication;
        private readonly IUserService _userService;
        private readonly IClientService _clientService;

        public AccountController(IFormsAuthentication formsAuthentication, IUserService userService, IClientService clientService)
        {
            _formsAuthentication = formsAuthentication;
            _userService = userService;
            _clientService = clientService;
        }

        //
        // GET: /Account/LogOn
        public ActionResult LogOn()
        {
            return View();
        }

        // GET: /Account/LogOn
        [Authorize]
        public ActionResult Admin()
        {
            var roleId = User.FriendlyIdentity().RoleId;
            if(roleId == (int)Role.Default)                
                    return RedirectToAction("NotAuthorized", "Account");
            var model = new AdministrationModel();
            if(roleId == (int)Role.Lawyer)
            {
                model.Clients = _clientService.GetUsersClients(User.FriendlyIdentity().Id);
                model.Email = new AdminEmail
                    {
                        UserId = User.FriendlyIdentity().Id
                    };
                return View(model);                                                
            }            
            return RedirectToAction("NotAuthorized", "Account");
        }

        public ActionResult NotAuthorized()
        {
            return View();
        }

        //
        // POST: /Account/LogOn
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.Authenticate(model.Email, model.Password);
                if (user != null)
                {
                    _formsAuthentication.SetAuthCookie(HttpContext, UserAuthenticationTicketBuilder.CreateAuthenticationTicket(user, model.RememberMe));
                    if (Url.IsValidReturnUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "The email or password is incorrect.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff
        public ActionResult LogOff()
        {
            _formsAuthentication.Signout();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                if (_userService.DoesNativeUserExist(model.Email))
                {
                    ModelState.AddModelError("", @"User with this e-mail address already exists.");
                }
                else
                {
                    //if/when we fix roles, we'll update it here
                    var user = _userService.CreateNativeUser(model.Email, model.FirstName, model.LastName, model.Password);
                    _formsAuthentication.SetAuthCookie(HttpContext, UserAuthenticationTicketBuilder.CreateAuthenticationTicket(user));
                    return RedirectToAction("Index", "Home");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
