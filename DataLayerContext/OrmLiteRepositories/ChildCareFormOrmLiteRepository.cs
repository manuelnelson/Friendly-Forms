using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class ChildCareFormOrmLiteRepository : FormOrmLiteRepository<ChildCareForm>, IChildCareFormRepository
    {
        public ChildCareFormOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
