using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class HealthInsuranceOrmLiteRepository : FormOrmLiteRepository<HealthInsurance>, IHealthInsuranceRepository
    {
        public HealthInsuranceOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
