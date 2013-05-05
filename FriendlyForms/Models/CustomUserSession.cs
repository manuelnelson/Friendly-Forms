﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common.Extensions;
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
            //var user = session.TranslateTo<User>();
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
            authService.SaveSession(session, TimeSpan.FromDays(7 * 2));
        }

    }
}