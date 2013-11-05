using System.Collections.Generic;
using DataInterface;
using Models;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface.Auth;
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
                return db.FirstOrDefault<User>(u=>u.UserAuthId == userAuthId);
            }  
        }

        public List<UserAuth> GetAttorneysClients(long id)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.GetList<UserAuth>(string.Format(@"select * from UserAuth where Id in (select UserAuthId from Users where Id in (Select ClientUserId from AttorneyClients where UserId = {0})", id));
            }
        }
    }
}
