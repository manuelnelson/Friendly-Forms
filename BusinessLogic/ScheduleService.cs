using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class ScheduleService : FormService<IScheduleRepository, Schedule>, IScheduleService
    {
        public ScheduleService(IScheduleRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
