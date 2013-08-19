using System.Collections.Generic;
using DataInterface;
using Models.Contract;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class FormOrmLiteRepository<TFormEntity> : OrmLiteRepository<TFormEntity>, IFormRepository<TFormEntity>
        where TFormEntity : class, IEntity, IFormEntity, new()
    {
        public FormOrmLiteRepository(IDbConnectionFactory dbFactory)
            : base(dbFactory)
        {
        }

        public TFormEntity GetByUserId(long userId)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.FirstOrDefault<TFormEntity>(x => x.UserId == userId); 
            }              
        }

        public List<TFormEntity> GetListByUserId(long userId)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.Select<TFormEntity>(x => x.UserId == userId);
            }
        }
    }
}