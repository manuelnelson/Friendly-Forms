using System;
using System.Linq;
using System.Net;
using BusinessLogic.Contracts;
using FriendlyForms.Models;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.WebHost.Endpoints;

namespace FriendlyForms.Helpers
{
    /// <summary>
    /// Will check if logged-in user (LIU) has access to client's information.  This checks if the LIU is the client, or 
    /// is located in the attorney-client relationship table
    /// </summary>
    public class CanViewClientInfo : RequestFilterAttribute
    {
        public override void Execute(IHttpRequest req, IHttpResponse res, object requestDto)
        {
            var session = req.GetSession();
            if (session == null || !session.IsAuthenticated)
                throw new HttpError(HttpStatusCode.Unauthorized, "You are not logged in.");
            var authUserId = Convert.ToInt64(((CustomUserSession)session).CustomId);
            var clientUserId = ((IHasUser)requestDto).UserId;
            if (authUserId == clientUserId)
                return;
            var attorneyClientService = AppHostBase.Resolve<IAttorneyClientService>();
            if (attorneyClientService.GetFiltered(x => x.ClientUserId == clientUserId && x.UserId == authUserId).Any())
                return;
            throw new HttpError(HttpStatusCode.Forbidden, "You do not have access to this resource");
        }
    }
}