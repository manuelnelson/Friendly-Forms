using System.Collections.Generic;
using BusinessLogic.Models;

namespace BusinessLogic.Contracts
{
    public interface IMenuService
    {
        List<MenuItem> Get(string route, long userId, bool showAdminMenu, bool showAttorneyMenu, bool isAuthenticated = false);
        bool HasScheduleE(long userId);
        bool HasScheduleB(long userId);
    }
}
