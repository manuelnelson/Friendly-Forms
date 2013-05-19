using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;

namespace FriendlyForms.Models
{
    public class CustomUserSession : AuthUserSession
    {
        private IUserService UserService { get; set; }
        public CustomUserSession(IUserService userServices)
        {
            UserService = userServices;
        }

        public CustomUserSession()
        {
            UserService = EndpointHost.AppHost.TryResolve<IUserService>();
        }

        public string CustomId { get; set; }

        public override void OnAuthenticated(IServiceBase authService, IAuthSession session, IOAuthTokens tokens, Dictionary<string, string> authInfo)
        {
            base.OnAuthenticated(authService, session, tokens, authInfo);

            //Populate all matching fields from this session to your own custom User table            
            var user = session.TranslateTo<User>();
            //if (AppHost.Config.AdminUserNames.Contains(session.UserAuthName)
            //    && !session.HasRole(RoleNames.Admin))
            //{
            //    using (var assignRoles = authService.ResolveService<AssignRolesService>())
            //    {
            //        assignRoles.Post(new AssignRoles
            //        {
            //            UserName = session.UserAuthName,
            //            Roles = { RoleNames.Admin }
            //        });
            //    }
            //}
            //Resolve the DbFactory from the IOC and persist the user info
            var newUser = UserService.CreateOrUpdate(user);
            ((CustomUserSession)session).CustomId = newUser.Id.ToString();

            authService.SaveSession(session, TimeSpan.FromDays(7 * 2));
        }

    }
}