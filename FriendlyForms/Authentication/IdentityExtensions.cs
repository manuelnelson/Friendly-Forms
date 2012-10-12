using System.Security.Principal;

namespace FriendlyForms.Authentication
{
    public static class PrincipalExensions
    {
        public static FriendlyIdentity FriendlyIdentity(this IPrincipal principal)
        {
            //Not sure if this is the best way to do this       
            return (FriendlyIdentity)principal.Identity;
        }
    }
}