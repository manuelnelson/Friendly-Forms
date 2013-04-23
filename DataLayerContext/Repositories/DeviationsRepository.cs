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

        public Deviations GetChildById(long childId, bool isOtherParent = false)
        {
            return GetDbSet().FirstOrDefault(c => c.ChildId == childId && c.IsOtherParent == isOtherParent);
        }
    }
}
