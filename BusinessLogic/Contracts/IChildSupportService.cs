using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IChildSupportService : IFormService<IChildSupportRepository, ChildSupport>
    {
    }
}
