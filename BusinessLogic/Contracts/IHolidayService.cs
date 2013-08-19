using System.Collections.Generic;
using DataInterface;
using Models;
using Models.ViewModels;

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

        /// <summary>
        /// Add the holiday information to the database.
        /// </summary>
        /// <param name="holiday"></param>
        Holiday AddOrUpdate(HolidayViewModel holiday);

        List<Holiday> GetChildrenListByUserId(long userId);
    }
}
