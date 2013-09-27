using System.Collections.Generic;
using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class PreexistingSupportChildOrmLiteRepository : FormOrmLiteRepository<PreexistingSupportChild>, IPreexistingSupportChildRepository
    {
        public PreexistingSupportChildOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<PreexistingSupportChild> GetChildrenById(long preexistingSupportId)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.Select<PreexistingSupportChild>(x => x.PreexistingSupportId == preexistingSupportId);
            }     
        }

        public void DeleteChildrenBySupportId(int preexistingSupportId)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                db.Delete<PreexistingSupportChild>(x => x.PreexistingSupportId == preexistingSupportId);
            }
        }
    }
}
