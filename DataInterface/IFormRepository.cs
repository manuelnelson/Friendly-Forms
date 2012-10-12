using System;
using System.Data.Entity;
using Models.Contract;

namespace DataInterface
{
    public interface IFormRepository<TFormEntity> : IRepository<TFormEntity>, IDisposable 
        where TFormEntity : class, IFormEntity
    {
        TFormEntity GetByUserId(int userId);
//        DbSet<TFormEntity> GetFormDbSet();

    }
}
