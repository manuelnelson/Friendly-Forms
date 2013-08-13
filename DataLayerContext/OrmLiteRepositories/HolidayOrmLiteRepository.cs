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

        public Holiday GetByChildId(long childId)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.FirstOrDefault<Holiday>(x => x.ChildId == childId);
            }  
        }
    }
}
