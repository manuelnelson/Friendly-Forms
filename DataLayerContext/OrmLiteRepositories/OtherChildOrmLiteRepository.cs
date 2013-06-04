using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class OtherChildOrmLiteRepository : FormOrmLiteRepository<OtherChild>, IOtherChildRepository
    {
        public OtherChildOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
