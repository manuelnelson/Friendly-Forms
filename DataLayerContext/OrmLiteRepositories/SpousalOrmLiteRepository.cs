using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class SpousalOrmLiteRepository : FormOrmLiteRepository<SpousalSupport>, ISpousalRepository
    {
        public SpousalOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
