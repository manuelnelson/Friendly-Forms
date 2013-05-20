using Models;

namespace DataInterface
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByUserAuthId(int userAuthId);
    }
}
