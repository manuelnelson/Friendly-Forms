using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IHolidayService : IFormService<HolidayRepository,Holiday>
    {
        /// <summary>
        /// Retreives holiday infromation by childId
        /// </summary>
        /// <param name="childId"></param>
        /// <returns></returns>
        Holiday GetByChildId(int childId);

        /// <summary>
        /// Add the holiday information to the database.
        /// </summary>
        /// <param name="holiday"></param>
        Holiday AddOrUpdate(HolidayViewModel holiday);

    }
}
