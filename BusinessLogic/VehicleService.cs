using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;

namespace BusinessLogic
{
    public class VehicleService : FormService<IVehicleRepository, Vehicle>, IVehicleService
    {
        public VehicleService(IVehicleRepository formRepository)
            : base(formRepository)
        {
        }

        public new IEnumerable<Vehicle> GetByUserId(long userId)
        {
            try
            {
                var vehicles = FormRepository.GetFiltered(v=>v.UserId==userId);
                return vehicles;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }
    }
}
