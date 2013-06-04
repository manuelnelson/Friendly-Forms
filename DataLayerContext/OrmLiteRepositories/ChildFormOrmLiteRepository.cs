using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class ChildFormOrmLiteRepository : FormOrmLiteRepository<ChildForm>, IChildFormRepository
    {
        public ChildFormOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
