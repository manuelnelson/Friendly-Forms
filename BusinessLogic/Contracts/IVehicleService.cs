using System.Collections.Generic;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IVehicleService : IFormService<IVehicleRepository, Vehicle>
    {
        Vehicle AddOrUpdate(VehicleViewModel model);
        new IEnumerable<Vehicle> GetByUserId(long userId);
    }
}
