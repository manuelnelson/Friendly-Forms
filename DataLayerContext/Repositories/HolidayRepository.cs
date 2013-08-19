using System.Collections.Generic;
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

        public List<Holiday> GetChildListByUserId(long userId)
        {
            return GetDbSet().Where(h => h.UserId == userId).ToList();
        }
    }
}
