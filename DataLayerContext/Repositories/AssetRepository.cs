using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class AssetRepository : FormRepository<Assets>, IAssetRepository
    {
        public AssetRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
