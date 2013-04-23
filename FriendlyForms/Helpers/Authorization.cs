using System.Security.Principal;
using BusinessLogic.Contracts;
using FriendlyForms.Authentication;
using ServiceStack.WebHost.Endpoints;

namespace FriendlyForms.Helpers
{
    public static class Authorization
    {
        private static readonly IClientService ClientService;
        static Authorization()
        {
            ClientService = AppHostBase.Resolve<IClientService>();
        }
        public static bool IsAuthorized(IPrincipal currentUser, int userId)
        {
            var loggedInUserId = currentUser.FriendlyIdentity().Id;
            return !(userId == loggedInUserId || ClientService.LawyerHasClient(loggedInUserId, userId));
        }
    }
}