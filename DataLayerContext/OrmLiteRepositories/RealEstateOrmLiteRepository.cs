using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class RealEstateOrmLiteRepository : FormOrmLiteRepository<Property>, IRealEstateRepository
    {
        public RealEstateOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
