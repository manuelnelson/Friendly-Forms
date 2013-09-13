using System.Collections.Generic;
using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class AttorneyClientOrmLiteRepository : OrmLiteRepository<AttorneyClient>, IAttorneyClientRepository
    {
        public AttorneyClientOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<AttorneyClient> GetByUserId(long userId)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.Select<AttorneyClient>(x => x.UserId == userId);
            }     

        }
    }
}
