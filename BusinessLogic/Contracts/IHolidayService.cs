using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IHolidayService : IService<HolidayRepository,Holiday>
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

    }
}
