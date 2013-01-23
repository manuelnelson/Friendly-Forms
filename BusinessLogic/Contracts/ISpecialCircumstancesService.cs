using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface ISpecialCircumstancesService : IService<ISpecialCircumstancesRepository, SpecialCircumstances>
    {
        SpecialCircumstancesViewModel GetByUserId(int userId, bool isOtherParent = false);
        SpecialCircumstances AddOrUpdate(SpecialCircumstancesViewModel model);

    }
}
