using System.Collections.Generic;
using BusinessLogic.Models;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IMenuService
    {
        List<MenuItem> GetMenuList(string route, User user, bool isAuthenticated = false);
    }
}
