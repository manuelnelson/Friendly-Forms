using System;
using System.Security.Principal;
using System.Web.Security;
using BusinessLogic.Models;
using Models.ViewModels;

namespace FriendlyForms.Authentication
{
    [Serializable]
    public class FriendlyIdentity : IIdentity
    {
        public string Name { get; private set; }
        public string AuthenticationType{get { return "Friendly Forms"; }}
        public bool IsAuthenticated{get { return true; }}
        public string DisplayName { get; private set; }
        public int Id { get; private set; }
        public int RoleId { get; private set; }

        public FriendlyIdentity(string name, string displayName, int userId, int roleId = (int)Role.Default)
        {
            Name = name;
            DisplayName = displayName;
            Id = userId;
            RoleId = roleId;
        }

        public FriendlyIdentity(string name, UserInfo userInfo)
            : this(name, userInfo.FirstName, userInfo.Id, userInfo.RoleId)
        {
            if (userInfo == null) throw new ArgumentNullException("userInfo");
            Id = userInfo.Id;
            DisplayName = userInfo.FirstName;
            Name = name;
            RoleId = RoleId;
        }

        public FriendlyIdentity(FormsAuthenticationTicket ticket)
            : this(ticket.Name, UserInfo.FromString(ticket.UserData))
        {
            if (ticket == null) throw new ArgumentNullException("ticket");
        }
    }
}
