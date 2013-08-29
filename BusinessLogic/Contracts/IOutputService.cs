using System.Collections.Generic;
using BusinessLogic.Models;

namespace BusinessLogic.Contracts
{
    public interface IOutputService
    {
        ScheduleB GetScheduleB(long userId, string parentName, bool isOtherParent = false);
        List<IncompleteForm> GetParentingIncompleteForms(long userId);
        List<IncompleteForm> GetDomesticIncompleteForms(long userId);
        List<IncompleteForm> GetFinancialIncompleteForms(long userId);
        List<IncompleteForm> GetStarterIncompleteForms(long userId);
    }
}
