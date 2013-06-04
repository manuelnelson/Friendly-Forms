using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class PreexistingSupportOrmLiteRepository : FormOrmLiteRepository<PreexistingSupport>, IPreexistingSupportRepository
    {
        public PreexistingSupportOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
