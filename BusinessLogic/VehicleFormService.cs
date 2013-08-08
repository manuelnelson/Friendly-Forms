using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class VehicleFormService : FormService<IVehicleFormRepository, VehicleForm>, IVehicleFormService
    {
        public VehicleFormService(IVehicleFormRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
