using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class ExtraDecisionOrmLiteRepository : FormOrmLiteRepository<ExtraDecisions>, IExtraDecisionRepository
    {
        public ExtraDecisionOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
