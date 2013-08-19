using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IOtherChildrenService : IFormService<IOtherChildrenRepository,OtherChildren>
    {
        OtherChildren GetByUserId(long userId, bool isOtherParent = false);
    }
}
