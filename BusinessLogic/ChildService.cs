using System;
using BusinessLogic.Contracts;
using BusinessLogic.Models;
using DataInterface;
using DataLayerContext.Repositories;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ChildService : FormService<ChildRepository, Child, ChildrenViewModel>, IChildService
    {
        public ChildService(ChildRepository formRepository) : base(formRepository)
        {
        }

        public Child AddOrUpdate(ChildrenViewModel model)
        {
            try
            {
                //Check if entity already exists and we need to update record
                var entity = model.ConvertToEntity() as Child;
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

        public new ChildrenViewModel GetByUserId(int userId)
        {
            try
            {
                var childList = FormRepository.GetByUserId(userId);
                return new ChildrenViewModel()
                    {
                        Children = childList
                    };
            }
            catch(Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to save child information", ex);                
            }
        }
    }
}
