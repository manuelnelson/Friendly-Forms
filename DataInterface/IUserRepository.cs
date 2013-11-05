using System.Collections.Generic;
using Models;
using ServiceStack.ServiceInterface.Auth;

namespace DataInterface
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByUserAuthId(int userAuthId);
        List<UserAuth> GetAttorneysClients(long id);
    }
}
