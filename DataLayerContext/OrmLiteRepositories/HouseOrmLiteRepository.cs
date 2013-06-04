using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class HouseOrmLiteRepository : FormOrmLiteRepository<House>, IHouseRepository
    {
        public HouseOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
