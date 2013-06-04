using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class ResponsibilityOrmLiteRepository : FormOrmLiteRepository<Responsibility>, IResponsibilityRepository
    {
        public ResponsibilityOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
