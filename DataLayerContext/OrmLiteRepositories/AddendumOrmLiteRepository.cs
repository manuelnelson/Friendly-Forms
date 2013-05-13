using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class AddendumOrmLiteRepository : FormOrmLiteRepository<Addendum>, IAddendumRepository
    {
        public AddendumOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
