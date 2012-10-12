using DataLayerContext;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IExtraHolidayService : IFormService<ExtraHolidayRepository,ExtraHoliday>
    {
        ExtraHolidayViewModel GetByChildId(int childId);
        ExtraHoliday AddOrUpdate(ExtraHolidayViewModel model);
    }
}
