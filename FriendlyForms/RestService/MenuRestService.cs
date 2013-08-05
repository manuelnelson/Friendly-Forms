using System.Collections.Generic;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    public class MenuRestService
    {
        //REST Resource DTO
        [Route("/Menus/")]
        public class MenuDto : IReturn<MenuDto>
        {
            public string Route { get; set; }
        }

        public class MenusService : Service
        {
            public IMenuService MenuService { get; set; } //Injected by IOC

            public object Get(MenuDto request)
            {
                return MenuService.Get(request.Id);
            }

            public object Get(MenuListDto request)
            {
                //TODO Do something more interested.  Add query possibly 
                return MenuService.GetFiltered(t => t.Id != 0);
            }

        }

    }

}
