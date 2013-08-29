using System.Collections.Generic;
using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IDecisionsService : IFormService<IDecisionRepository,Decisions>
    {
        Decisions GetByChildId(long childId);
        List<Decisions> GetChildrenListByUserId(long userId);
    }
}
