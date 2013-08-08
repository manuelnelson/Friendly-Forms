using System;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ChildService : FormService<IChildRepository, Child>, IChildService
    {
        private IChildRepository ChildRepository { get; set; }
        public ChildService(IChildRepository formRepository) : base(formRepository)
        {
            ChildRepository = formRepository;
        }

        public Child AddOrUpdate(ChildViewModel model)
        {
            try
            {
                //Check if entity already exists and we need to update record
                var entity = model.ConvertToEntity() as Child;
                var existEntity = ChildRepository.Get(model.Id);
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

        public new ChildViewModel GetByUserId(long userId)
        {
            try
            {
                var childList = ChildRepository.GetByUserId(userId);
                return new ChildViewModel()
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
