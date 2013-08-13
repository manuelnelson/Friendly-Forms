using System.Collections.Generic;
using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class ChildCareOrmLiteRepository : FormOrmLiteRepository<ChildCare>, IChildCareRepository
    {
        public ChildCareOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }

        public ChildCare GetChildById(long childId)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.FirstOrDefault<ChildCare>(x => x.ChildId == childId);
            }
        }

        public IEnumerable<ChildCare> GetAllByUserId(long userId)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.Select<ChildCare>(x => x.UserId == userId);
            }
        }
    }
}
