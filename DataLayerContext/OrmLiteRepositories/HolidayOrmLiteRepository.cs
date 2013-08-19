using System.Collections.Generic;
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

        public List<Holiday> GetChildListByUserId(long userId)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.Select<Holiday>(x => x.UserId == userId);
            }
        }
    }
}
