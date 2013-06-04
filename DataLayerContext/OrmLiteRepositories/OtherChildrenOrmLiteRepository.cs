using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class OtherChildrenOrmLiteRepository : FormOrmLiteRepository<OtherChildren>, IOtherChildrenRepository
    {
        public OtherChildrenOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
