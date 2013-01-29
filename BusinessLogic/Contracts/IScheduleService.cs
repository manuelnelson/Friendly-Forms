using BusinessLogic.Models;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IScheduleService : IFormService<ScheduleRepository, Schedule>
    {
    }
}
