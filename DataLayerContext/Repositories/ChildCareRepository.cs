using System.Linq;
using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class ChildCareRepository : FormRepository<ChildCare>, IChildCareRepository
    {
        public ChildCareRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ChildCare GetChildById(int childId)
        {
            return GetDbSet().Where(c => c.ChildId == childId).FirstOrDefault();
        }
    }
}