using BusinessLogic.Contracts;
using DataInterface;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class VehicleFormService : FormService<IVehicleFormRepository, VehicleForm, VehicleFormViewModel>, IVehicleFormService
    {
        public VehicleFormService(IVehicleFormRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
