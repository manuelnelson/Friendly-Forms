using System.Collections.Generic;
using BusinessLogic.Models;

namespace BusinessLogic.Contracts
{
    public interface IMenuService
    {
        List<MenuItem> Get(string route, long userId, bool isAuthenticated = false);
    }
}
