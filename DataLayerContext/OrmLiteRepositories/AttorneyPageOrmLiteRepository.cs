using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class AttorneyPageOrmLiteRepository : OrmLiteRepository<AttorneyPage>, IAttorneyPageRepository
    {
        public AttorneyPageOrmLiteRepository(IDbConnectionFactory dbFactory)
        : base(dbFactory)
    {
    }

    }
}

