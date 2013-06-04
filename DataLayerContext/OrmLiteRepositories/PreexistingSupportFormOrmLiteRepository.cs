using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class PreexistingSupportFormOrmLiteRepository : FormOrmLiteRepository<PreexistingSupportForm>, IPreexistingSupportFormRepository
    {
        public PreexistingSupportFormOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
