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
    }
}
