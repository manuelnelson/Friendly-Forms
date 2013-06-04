using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class ExtraExpenseFormOrmLiteRepository : FormOrmLiteRepository<ExtraExpenseForm>, IExtraExpenseFormRepository
    {
        public ExtraExpenseFormOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
