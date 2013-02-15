using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IDeviationsService : IFormService<IDeviationsRepository, Deviations>
    {
        DeviationsViewModel GetByUserId(int userId, bool isOtherParent = false);
        Deviations AddOrUpdate(DeviationsViewModel model);

    }
}
