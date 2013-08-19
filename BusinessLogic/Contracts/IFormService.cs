using System.Collections.Generic;
using DataInterface;
using Models.Contract;

namespace BusinessLogic.Contracts
{
    public interface IFormService<TRepository, TEntity> : IService<TRepository,TEntity>
        where TRepository : IFormRepository<TEntity>
        where TEntity : class, IEntity, IFormEntity 
    {
        /// <summary>
        /// Returns TViewModel infromation by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IFormEntity GetByUserId(long userId);

        List<IFormEntity> GetListByUserId(long userId);
    }
}
