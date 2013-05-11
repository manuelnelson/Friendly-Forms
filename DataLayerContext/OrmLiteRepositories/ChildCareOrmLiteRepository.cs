using System.Collections.Generic;
using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class ChildCareOrmLiteRepository : FormOrmLiteRepository<ChildCare>, IChildCareRepository
    {
        public ChildCareOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }

        public ChildCare GetChildById(long childId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ChildCare> GetAllByUserId(long userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
