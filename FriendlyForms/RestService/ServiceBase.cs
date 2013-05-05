using FriendlyForms.Models;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    public class ServiceBase : Service
    {
        public CustomUserSession UserSession
        {
            get { return SessionAs<CustomUserSession>(); }
        }
    }
}