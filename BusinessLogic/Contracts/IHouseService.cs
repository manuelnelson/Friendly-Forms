using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IHouseService : IFormService<IHouseRepository, House>
    {
    }
}
