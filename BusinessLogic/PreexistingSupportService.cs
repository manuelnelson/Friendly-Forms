using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Contracts;
using DataInterface;
using DataLayerContext.Repositories;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class PreexistingSupportService : FormService<IPreexistingSupportRepository, PreexistingSupport, PreexistingSupportViewModel>, IPreexistingSupportService
    {
        public PreexistingSupportService(IPreexistingSupportRepository formRepository)
            : base(formRepository)
        {
        }

        public List<PreexistingSupport> GetByUserId(long userId, bool isOtherParent = false)
        {
            try
            {
                return FormRepository.GetFiltered(m => m.UserId == userId && m.IsOtherParent ==isOtherParent).ToList();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }

        public PreexistingSupport AddOrUpdate(PreexistingSupportViewModel model)
        {
            try
            {
                //Check if entity already exists and we need to update record
                var entity = model.ConvertToEntity() as PreexistingSupport;
                var existEntity = FormRepository.GetFiltered(m => m.Id==entity.Id && m.IsOtherParent==entity.IsOtherParent).FirstOrDefault();
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
