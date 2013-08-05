using System.Collections.Generic;
using FriendlyForms.Models;
using ServiceStack.OrmLite;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;

namespace FriendlyForms.RestService
{
    [Route("/userauths")]
    public class UserAuths
    {
        public int[] Ids { get; set; }
    }
    [Route("/userauths/addrole")]
    public class UserRoles
    {
        public string[] Roles { get; set; }
    }
    public class UserAuthsResponse
    {
        public UserAuthsResponse()
        {
            this.UserAuths = new List<UserAuth>();
            this.OAuthProviders = new List<UserOAuthProvider>();
        }

        public List<UserAuth> UserAuths { get; set; }
        public CustomUserSession UserSession { get; set; }

        public List<UserOAuthProvider> OAuthProviders { get; set; }
    }

    //Implementation. Can be called via any endpoint or format, see: http://servicestack.net/ServiceStack.Hello/
    public class UserAuthsService : ServiceBase
    {
        public AssignRolesService AssignRolesService { get; set; }
        public object Any(UserAuths request)
        {
            var response = new UserAuthsResponse
            {
                UserSession = UserSession,
                UserAuths = Db.Select<UserAuth>(),
                OAuthProviders = Db.Select<UserOAuthProvider>(),
            };

            response.UserAuths.ForEach(x => x.PasswordHash = "[Redacted]");
            response.UserAuths.ForEach(x => x.Salt = "[Redacted]");

            return response;
        }

        [Authenticate]
        public object Post(UserRoles request)
        {
            AssignRolesService.Post(new AssignRoles
            {
                UserName = UserSession.UserAuthName,
                Roles = new List<string>(request.Roles)
            });
            return null;
        }
    }

}
