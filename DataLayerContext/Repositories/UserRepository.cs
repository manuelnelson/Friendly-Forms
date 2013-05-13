using System.Linq;
using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public User GetByUserAuthId(int userAuthId)
        {
            return GetDbSet().FirstOrDefault(u => u.UserAuthId == userAuthId);
        }
    }
}
