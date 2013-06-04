using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class InformationOrmLiteRepository : FormOrmLiteRepository<Information>, IInformationRepository
    {
        public InformationOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
