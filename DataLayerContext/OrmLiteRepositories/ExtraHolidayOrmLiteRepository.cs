using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class ExtraHolidayOrmLiteRepository : FormOrmLiteRepository<ExtraHoliday>, IExtraHolidayRepository
    {
        public ExtraHolidayOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
