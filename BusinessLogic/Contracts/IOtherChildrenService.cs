using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IOtherChildrenService : IService<IOtherChildrenRepository,OtherChildren>
    {
        OtherChildrenViewModel GetByUserId(int userId, bool isOtherParent = false);
    }
}
