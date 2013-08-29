using System.Collections.Generic;
using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IExtraDecisionsService : IFormService<IExtraDecisionRepository,ExtraDecisions>
    {
        List<ExtraDecisions> GetByChildId(long childId);
    }
}
