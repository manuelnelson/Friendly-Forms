using DataInterface;
using Models;
using ServiceStack.OrmLite;
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
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.First<User>(u=>u.UserAuthId == userAuthId);
            }  
        }
    }
}
