using System;
using BusinessLogic.Contracts;
using BusinessLogic.Properties;
using FriendlyForms.Models;
using ServiceStack.ServiceHost;

namespace FriendlyForms.RestService
{
    public class MenuRestService
    {
        //REST Resource DTO
        [Route("/Menus/")]
        public class MenuDto : IReturn<MenuDto>
        {
            public long UserId { get; set; }
            public string Route { get; set; }
        }

        public class MenusService : ServiceBase
        {
            public IMenuService MenuService { get; set; } //Injected by IOC

            public object Get(MenuDto request)
            {
                var showAdminMenu = request.UserId == Convert.ToInt64(UserSession.CustomId) &&
                                    UserSession.HasRole(Resources.AdminRole);
                var showAttorneyMenu = request.UserId == Convert.ToInt64(UserSession.CustomId) &&
                                       UserSession.HasRole(Resources.AttorneyRole);
                var hasPaid = ((CustomUserSession) UserSession).Paid;
                return MenuService.Get(request.Route, request.UserId, showAdminMenu, showAttorneyMenu,
                                       hasPaid, isAuthenticated: UserSession.IsAuthenticated);
            }

        }

    }

}
