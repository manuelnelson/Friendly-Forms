using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class VehicleRepository : FormRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
