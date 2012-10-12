using BusinessLogic.Models;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IExtraDecisionsService
    {
        ExtraDecisions AddOrUpdate(ExtraDecisionsViewModel model);
        ExtraDecisionsViewModel GetByChildId(int childId);
    }
}
