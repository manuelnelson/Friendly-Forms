using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ScheduleService : FormService<ScheduleRepository,Schedule,ScheduleViewModel>, IScheduleService
    {
        public ScheduleService(ScheduleRepository formRepository) : base(formRepository)
        {
        }
    }
}
