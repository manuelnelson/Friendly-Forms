using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DataInterface;
using Models.Contract;

namespace BusinessLogic.Contracts
{
    public interface IService<TRepository, TEntity> : IDisposable
        where TRepository : IFormRepository<TEntity>
        where TEntity : class, IFormEntity 
    {
        //TRepository FormRepository { get; set; }
        /// <summary>
        /// Add the entity to the database.
        /// </summary>
        /// <param name="item"></param>
        void Add(TEntity item);

        /// <summary>
        /// Add list of entities to the database
        /// </summary>
        /// <param name="items"></param>
        void AddAll(IEnumerable<TEntity> items);

        /// <summary>
        /// Gets the entity from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(long id);

        /// <summary>
        /// Gets the entity from the database
        /// </summary>        
        /// <param name="whereExpression">Where expression</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// Updates the entity from the database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        void Update(TEntity item);

        /// <summary>
        /// Deletes the entity from the database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        void Delete(TEntity item);

        /// <summary>
        /// Deletes a list of entites from the database
        /// </summary>
        /// <param name="items"></param>
        void DeleteAll(IEnumerable<TEntity> items);

        /// <summary>
        /// Deletes a list of entites by id from the database
        /// </summary>
        /// <param name="ids"></param>
        void DeleteAll(IEnumerable<long> ids);

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
        TEntity AddOrUpdate(IViewModel model);
    }
}
