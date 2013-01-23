using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IChildSupportService : IService<IChildSupportRepository, ChildSupport>
    {
    }
}
