using BusinessLogic.Models;

namespace BusinessLogic.Contracts
{
    public interface IOutputService
    {
        ScheduleB GetScheduleB(long userId, string parentName, bool isOtherParent = false);

    }
}
