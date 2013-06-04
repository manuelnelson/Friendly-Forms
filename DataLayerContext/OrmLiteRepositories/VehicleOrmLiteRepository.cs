using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class VehicleOrmLiteRepository : FormOrmLiteRepository<Vehicle>, IVehicleRepository
    {
        public VehicleOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
