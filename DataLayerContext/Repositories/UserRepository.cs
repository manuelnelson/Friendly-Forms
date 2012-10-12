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

        public bool NativeExists(string email)
        {
            return GetDbSet().Any(u => u.Email.ToUpper().Equals(email.ToUpper()) && u.Password != null);
        }

        public User GetByEmail(string email)
        {
            return GetDbSet().FirstOrDefault(u => u.Email.ToUpper().Equals(email.ToUpper()));
        }
    }
}
