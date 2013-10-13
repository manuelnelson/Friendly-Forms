using System.Collections.Generic;
using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class StateOrmLiteRepository : OrmLiteRepository<State>, IStateRepository
    {
        public StateOrmLiteRepository(IDbConnectionFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<State> GetAll()
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.Select<State>("Select * from States Order by Name");
            }
        }
    }
}
