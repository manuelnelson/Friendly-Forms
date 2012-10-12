using System.Linq;
using DataInterface;
using DataLayerContext.Repositories;
using Models;

namespace DataLayerContext
{
    public class ExtraHolidayRepository : FormRepository<ExtraHoliday>, IExtraHolidayRepository
    {
        public ExtraHolidayRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ExtraHoliday GetByChildId(int childId)
        {
            return GetDbSet().FirstOrDefault(e => e.ChildId.Equals(childId));
        }
    }
}
