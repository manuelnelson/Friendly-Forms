using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface ITaxService : IService<ITaxRepository, Tax>
    {
    }
}
