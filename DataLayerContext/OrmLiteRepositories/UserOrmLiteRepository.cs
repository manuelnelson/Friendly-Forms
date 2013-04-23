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

        public bool NativeExists(string email)
        {
            //return GetDbSet().Any(u => u.Email.ToUpper().Equals(email.ToUpper()) && u.Password != null);
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.First<User>(x => x.Email == email && x.Password != null) != null;
            }
        }

        public User GetByEmail(string email)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.First<User>(x => x.Email == email);
            }
        }
    }
}
