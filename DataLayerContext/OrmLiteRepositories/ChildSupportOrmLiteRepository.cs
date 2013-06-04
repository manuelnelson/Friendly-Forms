using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class ChildSupportOrmLiteRepository : FormOrmLiteRepository<ChildSupport>, IChildSupportRepository
    {
        public ChildSupportOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
