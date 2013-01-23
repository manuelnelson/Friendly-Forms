using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ScheduleService : Service<ScheduleRepository,Schedule,ScheduleViewModel>, IScheduleService
    {
        public ScheduleService(ScheduleRepository formRepository) : base(formRepository)
        {
        }
    }
}
