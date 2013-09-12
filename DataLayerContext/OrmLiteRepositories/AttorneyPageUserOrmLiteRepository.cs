using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class AttorneyPageUserOrmLiteRepository : OrmLiteRepository<AttorneyPageUser>, IAttorneyPageUserRepository
    {
        public AttorneyPageUserOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
