using System;
using System.Linq;
using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class SpecialCircumstancesService : FormService<SpecialCircumstancesRepository, SpecialCircumstances, SpecialCircumstancesViewModel>, ISpecialCircumstancesService
    {
        public SpecialCircumstancesService(SpecialCircumstancesRepository formRepository) : base(formRepository)
        {
        }

        public SpecialCircumstancesViewModel GetByUserId(int userId, bool isOtherParent = false)
        {
            try
            {
                var entity = FormRepository.GetFiltered(m => m.UserId==userId && m.IsOtherParent==isOtherParent).FirstOrDefault();
                return (entity == null ? new SpecialCircumstancesViewModel() : entity.ConvertToModel()) as SpecialCircumstancesViewModel;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }

        public SpecialCircumstances AddOrUpdate(SpecialCircumstancesViewModel model)
        {
            try
            {
                //Check if entity already exists and we need to update record
                var entity = model.ConvertToEntity() as SpecialCircumstances;
                var existEntity = FormRepository.GetFiltered(m => m.UserId==entity.UserId && m.IsOtherParent==entity.IsOtherParent).FirstOrDefault();
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
