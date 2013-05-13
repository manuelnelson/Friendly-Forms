using Models;

namespace BusinessLogic.Contracts
{
    public interface IUserService
    {
        User CreateOrUpdate(User user);
    }
}
