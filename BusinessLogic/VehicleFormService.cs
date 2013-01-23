using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class VehicleFormService : Service<VehicleFormRepository, VehicleForm, VehicleFormViewModel>, IVehicleFormService
    {
        public VehicleFormService(VehicleFormRepository formRepository) : base(formRepository)
        {
        }
    }
}
