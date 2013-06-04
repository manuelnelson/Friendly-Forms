using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class TaxOrmLiteRepository : FormOrmLiteRepository<Tax>, ITaxRepository
    {
        public TaxOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
