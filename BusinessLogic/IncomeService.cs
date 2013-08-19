using System;
using System.Linq;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class IncomeService : FormService<IIncomeRepository, Income>, IIncomeService
    {
        public IncomeService(IIncomeRepository formRepository)
            : base(formRepository)
        {
        }

        public Income GetByUserId(long userId, bool isOtherParent = false)
        {
            try
            {
                return FormRepository.GetFiltered(m=>m.UserId == userId && m.IsOtherParent == isOtherParent).FirstOrDefault();
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
