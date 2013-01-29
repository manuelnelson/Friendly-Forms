using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface ITaxService : IFormService<ITaxRepository, Tax>
    {
    }
}
