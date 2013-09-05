using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IUserService : IService<IUserRepository, User>
    {
        User CreateOrUpdate(User user);
    }
}
