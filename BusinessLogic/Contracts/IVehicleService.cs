using System.Collections.Generic;
using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IVehicleService : IFormService<IVehicleRepository, Vehicle>
    {
        new IEnumerable<Vehicle> GetByUserId(long userId);
    }
}
