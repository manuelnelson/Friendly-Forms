using System.Collections.Generic;
using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class ChildOrmLiteRepository : FormOrmLiteRepository<Child>, IChildRepository
    {
        public ChildOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }

        public new List<Child> GetByUserId(long userId)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.Select<Child>(x=>x.UserId == userId);
            }     
        }
    }
}
