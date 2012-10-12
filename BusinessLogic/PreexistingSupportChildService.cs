using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class PreexistingSupportChildService : FormService<PreexistingSupportChildRepository, PreexistingSupportChild, PreexistingSupportChildViewModel>, IPreexistingSupportChildService
    {
        public PreexistingSupportChildService(PreexistingSupportChildRepository formRepository) : base(formRepository)
        {
        }

        public IEnumerable<PreexistingSupportChild> GetChildrenBySupportId(int id)
        {
            try
            {
                return FormRepository.GetChildrenById(id);
            }
            catch(Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }

        public PreexistingSupportChild AddOrUpdate(PreexistingSupportChildViewModel model)
        {
            try
            {
                //Check if entity already exists and we need to update record
                var entity = model.ConvertToEntity() as PreexistingSupportChild;
                var existEntity = FormRepository.Get(model.Id);
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
