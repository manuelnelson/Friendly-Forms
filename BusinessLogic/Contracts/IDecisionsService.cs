using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IDecisionsService : IFormService<IDecisionRepository,Decisions>
    {
        void AddOrUpdate(DecisionsViewModel model);
        Decisions GetByChildId(int childId);
    }
}
