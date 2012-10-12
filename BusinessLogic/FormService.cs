using System;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models.Contract;

namespace BusinessLogic
{
    public class FormService<TFormRepository, TFormEntity, TViewModel> : IFormService<TFormRepository, TFormEntity>
        where TFormRepository : IFormRepository<TFormEntity>
        where TFormEntity : class, IFormEntity  
        where TViewModel : IViewModel, new()
    {
        public TFormRepository FormRepository { get; set; }

        public FormService(TFormRepository formRepository)
        {
            FormRepository = formRepository;
        }

        /// <summary>
        /// Returns TViewModel infromation by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IViewModel GetByUserId(int userId)
        {
            try
            {
                var entity = FormRepository.GetByUserId(userId);
                return entity == null ? new TViewModel() : entity.ConvertToModel();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }

        /// <summary>
        /// Add the TViewModel information to the database.
        /// </summary>
        /// <param name="model"></param>
        public TFormEntity AddOrUpdate(IViewModel model)
        {
            try
            {                
                //Check if entity already exists and we need to update record
                var entity = model.ConvertToEntity();
                var existEntity = FormRepository.GetByUserId(model.UserId);
                if (existEntity != null)
                {
                    existEntity.Update(entity);
                    FormRepository.Update(existEntity);
                    return existEntity;
                }
                //Add entity to database
                FormRepository.Add((TFormEntity) entity);
                return entity as TFormEntity;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to save", ex);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            
        }
    }
}
