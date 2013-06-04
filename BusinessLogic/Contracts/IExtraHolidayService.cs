using System.Collections.Generic;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IExtraHolidayService : IFormService<IExtraHolidayRepository,ExtraHoliday>
    {
        List<ExtraHoliday> GetByChildId(long childId);
        ExtraHoliday AddOrUpdate(ExtraHolidayViewModel model);
    }
}
