using System;
using DataInterface;
using Models.Contract;

namespace BusinessLogic.Contracts
{
    public interface IFormService<TRepository, TFormEntity> : IDisposable
        where TRepository : IFormRepository<TFormEntity>
        where TFormEntity : class, IFormEntity 
    {
        //TRepository FormRepository { get; set; }

        /// <summary>
        /// Returns TViewModel infromation by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IViewModel GetByUserId(int userId);

        /// <summary>
        /// Add the TViewModel information to the database.
        /// </summary>
        /// <param name="model"></param>
        TFormEntity AddOrUpdate(IViewModel model);
    }
}
