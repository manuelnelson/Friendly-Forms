using System.Collections.Generic;
using Models;

namespace DataInterface
{
    public interface IHolidayRepository : IFormRepository<Holiday>
    {
        Holiday GetByChildId(long childId);
        List<Holiday> GetChildListByUserId(long userId);
    }
}
