using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class VehicleFormRepository : FormRepository<VehicleForm>, IVehicleFormRepository
    {
        public VehicleFormRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
