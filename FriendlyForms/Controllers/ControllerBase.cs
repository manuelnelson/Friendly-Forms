using System.Web.Mvc;
using FriendlyForms.Models;
using ServiceStack.Mvc;
using ServiceStack.Mvc.MiniProfiler;

namespace FriendlyForms.Controllers
{
    [ProfilingActionFilter]
    public class ControllerBase : ServiceStackController<CustomUserSession>
    {
        
        public ActionResult RedirectIfNotAuthenticated(string returnUrl)
        {
            int userId;
            if (!UserSession.IsAuthenticated || !int.TryParse(UserSession.CustomId, out userId))
            {
                return RedirectToAction("LogOn", "Account", new { Continue = returnUrl });
            }
            return null;
        }
    }
}