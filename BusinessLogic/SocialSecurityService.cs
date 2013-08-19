using System;
using System.Linq;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class SocialSecurityService : FormService<ISocialSecurityRepository, SocialSecurity>, ISocialSecurityService
    {
        public SocialSecurityService(ISocialSecurityRepository formRepository)
            : base(formRepository)
        {
        }

        public SocialSecurity GetByUserId(long userId, bool isOtherParent = false)
        {
            try
            {
                return FormRepository.GetFiltered(m => m.UserId==userId && m.IsOtherParent==isOtherParent).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }

        public SocialSecurity AddOrUpdate(SocialSecurityViewModel model)
        {
            try
            {
                //Check if entity already exists and we need to update record
                var entity = model.ConvertToEntity() as SocialSecurity;
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
