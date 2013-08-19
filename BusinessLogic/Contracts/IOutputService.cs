using System.Collections.Generic;
using BusinessLogic.Models;

namespace BusinessLogic.Contracts
{
    public interface IOutputService
    {
        ScheduleB GetScheduleB(long userId, string parentName, bool isOtherParent = false);
        List<string> GetParentingIncompleteForms(long userId);
        List<string> GetDomesticIncompleteForms(long userId);
        List<string> GetFinancialIncompleteForms(long userId);
    }
}
