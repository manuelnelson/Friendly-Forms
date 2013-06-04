using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class DebtOrmLiteRepository : FormOrmLiteRepository<Debt>, IDebtRepository
    {
        public DebtOrmLiteRepository(IDbConnectionFactory dbFactory): base(dbFactory)
        {
        }
    }
}
