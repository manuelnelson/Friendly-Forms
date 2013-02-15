using System.Collections.Generic;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IExtraDecisionsService 
    {
        ExtraDecisions AddOrUpdate(ExtraDecisionsViewModel model);
        List<ExtraDecisions> GetByChildId(long childId);
    }
}
