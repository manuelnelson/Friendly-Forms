using System.Security.Principal;
using BusinessLogic.Contracts;
using FriendlyForms.Authentication;

namespace FriendlyForms.Helpers
{
    public static class Authorization
    {
        private static readonly IClientService ClientService;
        static Authorization()
        {
            var adapter = new MunqIocAdapter();
            ClientService = adapter.TryResolve<IClientService>();
        }
        public static bool IsAuthorized(IPrincipal currentUser, int userId)
        {
            var loggedInUserId = currentUser.FriendlyIdentity().Id;
            return !(userId == loggedInUserId || ClientService.LawyerHasClient(loggedInUserId, userId));
        }
    }
}