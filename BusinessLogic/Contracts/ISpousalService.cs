using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface ISpousalService : IFormService<ISpousalRepository, SpousalSupport>
    {
    }
}
