using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class AssetService : FormService<AssetRepository, Assets, AssetViewModel>, IAssetService
    {
        public AssetService(AssetRepository formRepository) : base(formRepository)
        {
        }
    }
}
