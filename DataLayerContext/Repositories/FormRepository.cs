using System.Collections.Generic;
using System.Linq;
using DataInterface;
using Models.Contract;

namespace DataLayerContext.Repositories
{
    public class FormRepository<TFormEntity> : Repository<TFormEntity>, IFormRepository<TFormEntity>
        where TFormEntity : class, IEntity, IFormEntity 
    {
        public FormRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public TFormEntity GetByUserId(long userId)
        {
            return GetDbSet().FirstOrDefault(u => u.UserId == userId);
        }

        public List<TFormEntity> GetListByUserId(long userId)
        {
            return GetDbSet().Where(u => u.UserId == userId).ToList();
        }
    }
}
