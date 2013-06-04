using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class DeviationsOrmLiteRepository : FormOrmLiteRepository<Deviations>, IDeviationsRepository
    {
        public DeviationsOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }

        public Deviations GetChildById(long childId, bool isOtherParent = false)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.First<Deviations>(x => x.ChildId == childId);
            }  
        }
    }
}
