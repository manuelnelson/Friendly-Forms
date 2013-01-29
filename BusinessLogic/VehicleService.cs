using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class VehicleService : FormService<VehicleRepository, Vehicle, VehicleViewModel>, IVehicleService
    {
        public VehicleService(VehicleRepository formRepository) : base(formRepository)
        {
        }

        public Vehicle AddOrUpdate(VehicleViewModel model)
        {
            try
            {
                //Check if entity already exists and we need to update record
                var entity = model.ConvertToEntity();
                var existEntity = FormRepository.Get(model.Id);
                if (existEntity != null)
                {
                    existEntity.Update(entity);
                    FormRepository.Update(existEntity);
                    return existEntity;
                }
                //Add entity to database
                FormRepository.Add((Vehicle) entity);
                return (Vehicle) entity;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to save", ex);
            }
        }

        public new IEnumerable<Vehicle> GetByUserId(int userId)
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
