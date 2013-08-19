using System.Collections.Generic;
using DataInterface;
using Models;
using Models.Contract;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class DecisionOrmLiteRepository : FormOrmLiteRepository<Decisions>, IDecisionRepository
    {
        public DecisionOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }

        public Decisions GetByChildId(long childId)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.FirstOrDefault<Decisions>(x => x.ChildId == childId);
            }  
        }

        public List<Decisions> GetChildListByUserId(long userId)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.Select<Decisions>(x => x.UserId == userId);
            }
        }
    }
}
