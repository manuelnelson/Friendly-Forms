using System;
using System.Web.Security;
using BusinessLogic.Models;
using ServiceStack.Common.Extensions;

namespace FriendlyForms.Authentication
{
    public class UserAuthenticationTicketBuilder
    {
        /// <summary>
        /// Creates a new <see cref="FormsAuthenticationTicket"/> from a user.
        /// </summary>
        /// <param name="accountUser"></param>
        /// <param name="isPersistent">Whether or not ticket will be persistent across browser sessions. </param>
        /// <returns></returns>
        /// <remarks>
        /// Encodes the <see cref="UserInfo"/> into the <see cref="FormsAuthenticationTicket.UserData"/> property
        /// of the authentication ticket.  This can be recovered by using the <see cref="UserInfo.FromString"/> method.
        /// </remarks>
        public static FormsAuthenticationTicket CreateAuthenticationTicket(AccountUser accountUser, bool isPersistent = false)
        {
            var userInfo = CreateUserContextFromUser(accountUser);

            var ticket = new FormsAuthenticationTicket(
                1,
                accountUser.Email, //Originally had this as authorizationId, got rid of it b/c native users don't have this.  E-mail is our unique identifier.
                DateTime.Now,
                DateTime.Now.Add(FormsAuthentication.Timeout),
                isPersistent,
                userInfo.ToString());

            return ticket;
        }

        private static UserInfo CreateUserContextFromUser(AccountUser accountUser)
        {
            return accountUser.TranslateTo<UserInfo>();
        }
    }
}