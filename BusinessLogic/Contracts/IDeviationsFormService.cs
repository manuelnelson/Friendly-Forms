using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IDeviationsFormService : IFormService<IDeviationsFormRepository, DeviationsForm>
    {
        DeviationsFormViewModel GetByUserId(long userId, bool isOtherParent = false);
    }
}
