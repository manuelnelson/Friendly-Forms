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
        IViewModel GetByUserId(long userId);

        /// <summary>
        /// Add the TViewModel information to the database.
        /// </summary>
        /// <param name="model"></param>
        TEntity AddOrUpdate(IViewModel model);
    }
}
