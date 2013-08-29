using System.Collections.Generic;
using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IHolidayService : IFormService<IHolidayRepository,Holiday>
    {
        /// <summary>
        /// Retreives holiday infromation by childId
        /// </summary>
        /// <param name="childId"></param>
        /// <returns></returns>
        Holiday GetByChildId(long childId);

        List<Holiday> GetChildrenListByUserId(long userId);
    }
}
