using System.Collections.Generic;
using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IUserService : IService<IUserRepository, User>
    {
        User CreateOrUpdate(User user);
        User GetByUserAuthId(int userAuthId);
        List<User> GetTodaysActiveAccounts();
        int GetNumberOfUsersAddedThisMonth(User adminUser);
    }
}
