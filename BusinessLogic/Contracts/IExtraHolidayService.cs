using System.Collections.Generic;
using DataLayerContext;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IExtraHolidayService : IFormService<ExtraHolidayRepository,ExtraHoliday>
    {
        List<ExtraHoliday> GetByChildId(long childId);
        ExtraHoliday AddOrUpdate(ExtraHolidayViewModel model);
    }
}
