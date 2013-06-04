using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class CommunicationOrmLiteRepository : FormOrmLiteRepository<Communication>, ICommunicationRepository
    {
        public CommunicationOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
