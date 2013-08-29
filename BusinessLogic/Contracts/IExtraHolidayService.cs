using System.Collections.Generic;
using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IExtraHolidayService : IFormService<IExtraHolidayRepository,ExtraHoliday>
    {
        List<ExtraHoliday> GetByChildId(long childId);
    }
}
