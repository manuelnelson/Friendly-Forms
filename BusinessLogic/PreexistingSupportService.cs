using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class PreexistingSupportService : Service<PreexistingSupportRepository, PreexistingSupport, PreexistingSupportViewModel>, IPreexistingSupportService
    {
        public PreexistingSupportService(PreexistingSupportRepository formRepository) : base(formRepository)
        {
        }

        public List<PreexistingSupport> GetByUserId(int userId, bool isOtherParent = false)
        {
            try
            {
                return FormRepository.GetFiltered(m => m.UserId.Equals(userId) && m.IsOtherParent.Equals(isOtherParent)).ToList();
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
                var existEntity = FormRepository.GetFiltered(m => m.Id.Equals(entity.Id) && m.IsOtherParent.Equals(entity.IsOtherParent)).FirstOrDefault();
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
