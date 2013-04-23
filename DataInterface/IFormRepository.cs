using Models.Contract;

namespace DataInterface
{
    public interface IFormRepository<TFormEntity> : IRepository<TFormEntity> 
        where TFormEntity : IEntity, IFormEntity
    {
        TFormEntity GetByUserId(long userId);
    }
}
