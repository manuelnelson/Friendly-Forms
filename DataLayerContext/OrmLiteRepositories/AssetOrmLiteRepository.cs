using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class AssetOrmLiteRepository : FormOrmLiteRepository<Assets>, IAssetRepository
    {
        public AssetOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
