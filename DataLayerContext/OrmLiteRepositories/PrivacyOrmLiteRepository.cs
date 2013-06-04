using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class PrivacyOrmLiteRepository : FormOrmLiteRepository<Privacy>, IPrivacyRepository
    {
        public PrivacyOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
