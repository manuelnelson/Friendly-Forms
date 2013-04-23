using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IOtherChildrenService : IFormService<IOtherChildrenRepository,OtherChildren>
    {
        OtherChildrenViewModel GetByUserId(long userId, bool isOtherParent = false);
    }
}
