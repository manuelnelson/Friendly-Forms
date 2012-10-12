using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IAssetService : IFormService<IAssetRepository, Assets>
    {
    }
}
