using System.Linq;
using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class ScheduleRepository : FormRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
