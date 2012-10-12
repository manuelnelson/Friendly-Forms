using System.Linq;
using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class DecisionRepository : FormRepository<Decisions>, IDecisionRepository
    {
        public DecisionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Decisions GetByChildId(int childId)
        {
            return GetDbSet().FirstOrDefault(d => d.ChildId.Equals(childId));
        }
    }
}
