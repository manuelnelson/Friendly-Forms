using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models.Contract;
using ServiceStack.Common.Web;

namespace BusinessLogic
{
    public class Service<TFormRepository, TEntity, TViewModel> : IService<TFormRepository, TEntity>
        where TFormRepository : IFormRepository<TEntity>
        where TEntity : class, IFormEntity  
        where TViewModel : IViewModel, new()
    {
        public TFormRepository FormRepository { get; set; }

        public Service(TFormRepository formRepository)
        {
            FormRepository = formRepository;
        }

        public void Add(TEntity item)
        {
            try
            {
                if (item != null) FormRepository.Add(item);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new HttpError(HttpStatusCode.InternalServerError, "Unable to add item");
            }
        }

        public void AddAll(IEnumerable<TEntity> items)
        {
            try
            {
                FormRepository.AddAll(items);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new HttpError(HttpStatusCode.InternalServerError, "Unable to add items");
            }
        }

        public TEntity Get(long id)
        {
            try
            {
                return FormRepository.Get(id);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new HttpError(HttpStatusCode.InternalServerError, "Unable to retrieve item");
            }
        }

        public IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                return FormRepository.GetFiltered(whereExpression);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new HttpError(HttpStatusCode.InternalServerError, "Unable to retrieve items");
            }
        }

        public void Update(TEntity item)
        {
            try
            {
                FormRepository.Update(item);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new HttpError(HttpStatusCode.InternalServerError, "Unable to update item");
            }
        }

        public void Delete(TEntity item)
        {
            try
            {
                FormRepository.Remove(item);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new HttpError(HttpStatusCode.InternalServerError, "Unable to remove item");
            }
        }

        public void DeleteAll(IEnumerable<TEntity> items)
        {
            try
            {
                FormRepository.RemoveAll(items);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new HttpError(HttpStatusCode.InternalServerError, "Unable to remove items");
            }
        }

        public void DeleteAll(IEnumerable<long> ids)
        {
            try
            {
                FormRepository.RemoveAll(ids);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new HttpError(HttpStatusCode.InternalServerError, "Unable to remove items");
            }
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
        public TEntity AddOrUpdate(IViewModel model)
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
                FormRepository.Add((TEntity) entity);
                return entity as TEntity;
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
