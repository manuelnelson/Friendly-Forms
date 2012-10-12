using Models;

namespace DataInterface
{
    public interface IUserRepository : IRepository<User>
    {
        bool NativeExists(string email);
        User GetByEmail(string email);
    }
}
