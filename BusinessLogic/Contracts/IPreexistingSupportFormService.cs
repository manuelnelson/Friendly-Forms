using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IPreexistingSupportFormService : IFormService<IPreexistingSupportFormRepository, PreexistingSupportForm>
    {
        PreexistingSupportFormViewModel GetByUserId(long userId, bool isOtherParent = false);
    }
}
