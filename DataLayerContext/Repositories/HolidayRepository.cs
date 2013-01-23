using System.Linq;
using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class HolidayRepository : FormRepository<Holiday>, IHolidayRepository
    {
        public HolidayRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Holiday GetByChildId(long childId)
        {
            return GetDbSet().FirstOrDefault(h => h.ChildId == childId);
        }
    }
}
