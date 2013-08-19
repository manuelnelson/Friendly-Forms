using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class DeviationsOrmLiteRepository : FormOrmLiteRepository<Deviations>, IDeviationsRepository
    {
        public DeviationsOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }

    }
}
