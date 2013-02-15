using System;
using System.Linq;
using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class DeviationsService : FormService<DeviationsRepository, Deviations, DeviationsViewModel>, IDeviationsService
    {
        public DeviationsService(DeviationsRepository formRepository) : base(formRepository)
        {
        }

        public DeviationsViewModel GetByUserId(int userId, bool isOtherParent = false)
        {
            try
            {
                var entity = FormRepository.GetFiltered(m => m.UserId==userId).FirstOrDefault();
                return (entity == null ? new DeviationsViewModel() : entity.ConvertToModel()) as DeviationsViewModel;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }

        public Deviations AddOrUpdate(DeviationsViewModel model)
        {
            try
            {
                //Check if entity already exists and we need to update record
                var entity = model.ConvertToEntity() as Deviations;
                var existEntity = FormRepository.GetFiltered(m => m.UserId==entity.UserId).FirstOrDefault();
                if (existEntity != null)
                {
                    existEntity.Update(entity);
                    FormRepository.Update(existEntity);
                    return existEntity;
                }
                //Add entity to database
                FormRepository.Add(entity);
                return entity;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to save", ex);
            }
        }
    }
}
