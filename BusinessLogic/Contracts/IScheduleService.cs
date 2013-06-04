using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IScheduleService : IFormService<IScheduleRepository, Schedule>
    {
    }
}
