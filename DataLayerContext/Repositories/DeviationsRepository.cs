using System.Linq;
using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class DeviationsRepository : FormRepository<Deviations>, IDeviationsRepository
    {
        public DeviationsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Deviations GetChildById(long childId)
        {
            return GetDbSet().FirstOrDefault(c => c.ChildId == childId);
        }
    }
}
