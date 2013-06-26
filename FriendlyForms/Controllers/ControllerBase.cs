using System.Web.Mvc;
using FriendlyForms.Models;
using ServiceStack.Mvc;
using ServiceStack.Mvc.MiniProfiler;

namespace FriendlyForms.Controllers
{
    [ProfilingActionFilter]
    public class ControllerBase : ServiceStackController<CustomUserSession>
    {
        
        public override ActionResult AuthenticationErrorResult
        {
            get
            {
                if (AuthSession == null || AuthSession.IsAuthenticated == false)
                {
                    return RedirectToAction("LogOn", "Account");
                }
                return base.AuthenticationErrorResult;
            }
        }
    }
}