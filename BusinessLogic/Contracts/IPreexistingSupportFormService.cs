using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IPreexistingSupportFormService : IFormService<IPreexistingSupportFormRepository, PreexistingSupportForm>
    {
        PreexistingSupportForm GetByUserId(long userId, bool isOtherParent = false);
    }
}
