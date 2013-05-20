using DataInterface;
using Models;
using IDbConnectionFactory = ServiceStack.OrmLite.IDbConnectionFactory;

namespace DataLayerContext.OrmLiteRepositories
{
    public class UserOrmLiteRepository : OrmLiteRepository<User>, IUserRepository
    {
        public UserOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }

        public User GetByUserAuthId(int userAuthId)
        {
            throw new System.NotImplementedException();
        }
    }
}
