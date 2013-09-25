using BusinessLogic.Models;

namespace BusinessLogic.Contracts
{
    public interface IBcsoService
    {
        double GetAmount(double income, int numberOfChildren);
        double GetAmount(ScheduleB scheduleB);

    }
}
