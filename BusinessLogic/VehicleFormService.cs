using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class VehicleFormService : FormService<VehicleFormRepository, VehicleForm, VehicleFormViewModel>, IVehicleFormService
    {
        public VehicleFormService(VehicleFormRepository formRepository) : base(formRepository)
        {
        }
    }
}
