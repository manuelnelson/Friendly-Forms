using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class ScheduleOrmLiteRepository : FormOrmLiteRepository<Schedule>, IScheduleRepository
    {
        public ScheduleOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
