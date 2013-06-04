using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class HealthOrmLiteRepository : FormOrmLiteRepository<Health>, IHealthRepository
    {
        public HealthOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
