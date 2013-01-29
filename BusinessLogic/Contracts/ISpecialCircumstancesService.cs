using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface ISpecialCircumstancesService : IFormService<ISpecialCircumstancesRepository, SpecialCircumstances>
    {
        SpecialCircumstancesViewModel GetByUserId(int userId, bool isOtherParent = false);
        SpecialCircumstances AddOrUpdate(SpecialCircumstancesViewModel model);

    }
}
