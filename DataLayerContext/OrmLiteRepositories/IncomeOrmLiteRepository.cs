using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class IncomeOrmLiteRepository : FormOrmLiteRepository<Income>, IIncomeRepository
    {
        public IncomeOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
