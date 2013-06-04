using BusinessLogic.Contracts;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class AssetService : FormService<IAssetRepository, Assets, AssetViewModel>, IAssetService
    {
        public AssetService(IAssetRepository formRepository) : base(formRepository)
        {
        }
    }
}
