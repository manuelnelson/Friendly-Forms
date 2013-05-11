using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class CourtOrmLiteRepository : FormOrmLiteRepository<Court>, ICourtRepository
    {
        public CourtOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
