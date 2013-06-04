using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class HolidayOrmLiteRepository : FormOrmLiteRepository<Holiday>, IHolidayRepository
    {
        public HolidayOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
