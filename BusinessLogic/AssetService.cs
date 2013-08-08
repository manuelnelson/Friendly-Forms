using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class AssetService : FormService<IAssetRepository, Assets>, IAssetService
    {
        public AssetService(IAssetRepository formRepository) : base(formRepository)
        {
        }
    }
}
