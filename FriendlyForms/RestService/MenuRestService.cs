﻿using System;
using BusinessLogic.Contracts;
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
                return MenuService.Get(request.Route, Convert.ToInt32(UserSession.CustomId), UserSession.IsAuthenticated);
            }

        }

    }

}