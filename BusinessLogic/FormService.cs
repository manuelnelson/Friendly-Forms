using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models.Contract;

namespace BusinessLogic
{
    public class FormService<TFormRepository, TEntity> : Service<TFormRepository, TEntity>, IFormService<TFormRepository, TEntity> 
        where TFormRepository : IFormRepository<TEntity>
        where TEntity : class, IEntity, IFormEntity
    {
        public TFormRepository FormRepository { get; set; }
        public FormService(TFormRepository formRepository) : base(formRepository)
        {
            FormRepository = formRepository;
        }

        /// <summary>
        /// Returns TViewModel infromation by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IFormEntity GetByUserId(long userId)
        {
            try
            {
                return FormRepository.GetByUserId(userId);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }

        public List<IFormEntity> GetListByUserId(long userId)
        {
            try
            {
                return FormRepository.GetListByUserId(userId) as List<IFormEntity>;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }
    }
}
