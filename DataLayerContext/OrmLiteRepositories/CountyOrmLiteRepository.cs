using System.Collections.Generic;
using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class CountyOrmLiteRepository : OrmLiteRepository<County>, ICountyRepository
    {
        public CountyOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<County> GetAll()
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.Select<County>("Select * from Counties Order by CountyName");
            }    
        }
    }
}
