using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface ISpousalService : IService<ISpousalRepository, SpousalSupport>
    {
    }
}
