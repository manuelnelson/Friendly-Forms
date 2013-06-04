using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class DeviationsFormOrmLiteRepository : FormOrmLiteRepository<DeviationsForm>, IDeviationsFormRepository
    {
        public DeviationsFormOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
