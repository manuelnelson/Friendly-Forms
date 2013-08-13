using Models;

namespace DataInterface
{
    public interface IHolidayRepository : IFormRepository<Holiday>
    {
        Holiday GetByChildId(long childId);
    }
}
