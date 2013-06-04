using BusinessLogic.Contracts;
using DataInterface;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ScheduleService : FormService<IScheduleRepository,Schedule,ScheduleViewModel>, IScheduleService
    {
        public ScheduleService(IScheduleRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
