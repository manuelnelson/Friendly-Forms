using System.Collections.Generic;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IVehicleService : IService<IVehicleRepository, Vehicle>
    {
        Vehicle AddOrUpdate(VehicleViewModel model);
        new IEnumerable<Vehicle> GetByUserId(int userId);
    }
}
