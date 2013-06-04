using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class VehicleFormOrmLiteRepository : FormOrmLiteRepository<VehicleForm>, IVehicleFormRepository
    {
        public VehicleFormOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
