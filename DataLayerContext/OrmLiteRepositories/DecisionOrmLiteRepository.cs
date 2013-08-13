using DataInterface;
using Models;
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
    }
}
