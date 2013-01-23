using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IDecisionsService : IService<IDecisionRepository,Decisions>
    {
        void AddOrUpdate(DecisionsViewModel model);
        Decisions GetByChildId(long childId);
    }
}
