using System;
using System.Security.Principal;
using System.Web.Security;
using BusinessLogic.Models;

namespace FriendlyForms.Authentication
{
    [Serializable]
    public class FriendlyIdentity : IIdentity
    {
        public FriendlyIdentity(string name, string displayName, int userId)
        {
            this.Name = name;
            this.DisplayName = displayName;
            this.UserId = userId;
        }

        public FriendlyIdentity(string name, UserInfo userInfo)
            : this(name, userInfo.DisplayName, userInfo.UserId)
        {
            if (userInfo == null) throw new ArgumentNullException("userInfo");
            this.UserId = userInfo.UserId;
            this.DisplayName = userInfo.DisplayName;
            this.Name = name;
        }

        public FriendlyIdentity(FormsAuthenticationTicket ticket)
            : this(ticket.Name, UserInfo.FromString(ticket.UserData))
        {
            if (ticket == null) throw new ArgumentNullException("ticket");
        }

        public string Name { get; private set; }

        public string AuthenticationType
        {
            get { return "Friendly Forms"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public string DisplayName { get; private set; }

        public int UserId { get; private set; }
    }
}
