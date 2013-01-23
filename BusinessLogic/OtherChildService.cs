using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class OtherChildService : Service<OtherChildRepository, OtherChild, OtherChildViewModel>, IOtherChildService
    {
        public OtherChildService(OtherChildRepository formRepository) : base(formRepository)
        {
        }

        public OtherChild AddOrUpdate(OtherChildViewModel model)
        {
            try
            {
                //Check if entity already exists and we need to update record
                var entity = model.ConvertToEntity() as OtherChild;
                if(model.Id != 0)
                {
                    var existEntity = FormRepository.Get(model.Id);
                    if (existEntity != null)
                    {
                        existEntity.Update(entity);
                        FormRepository.Update(existEntity);
                        return existEntity;
                    }
                }
                //Add entity to database
                FormRepository.Add(entity);
                return entity;

            }
            catch(Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to save", ex);  
            }
        }

        public IEnumerable<OtherChild> GetChildrenByOtherChildrenId(long otherChildrenId)
        {
            try
            {
                return FormRepository.GetFiltered(c => c.OtherChildrenId.Equals(otherChildrenId));
            }
            catch(Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve children information", ex);
            }
        }
    }
}
