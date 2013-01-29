using System;
using System.Linq;
using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class IncomeService : FormService<IncomeRepository, Income, IncomeViewModel>, IIncomeService
    {
        public IncomeService(IncomeRepository formRepository) : base(formRepository)
        {
        }

        public IncomeViewModel GetByUserId(int userId, bool isOtherParent = false)
        {
            try
            {
                var entity = FormRepository.GetFiltered(m=>m.UserId == userId && m.IsOtherParent == isOtherParent).FirstOrDefault();
                return (entity == null ? new IncomeViewModel() : entity.ConvertToModel()) as IncomeViewModel;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }

        public Income AddOrUpdate(IncomeViewModel model)
        {
            try
            {
                //Check if entity already exists and we need to update record
                var entity = model.ConvertToEntity() as Income;
                var existEntity = FormRepository.GetFiltered(m => m.UserId == entity.UserId && m.IsOtherParent == entity.IsOtherParent).FirstOrDefault(); 
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
