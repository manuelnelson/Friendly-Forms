using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IVehicleFormService : IFormService<IVehicleFormRepository, VehicleForm>
    {
    }
}
