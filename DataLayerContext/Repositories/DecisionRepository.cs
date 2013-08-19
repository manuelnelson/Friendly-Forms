using System.Collections.Generic;
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

        public Decisions GetByChildId(long childId)
        {
            return GetDbSet().FirstOrDefault(d => d.ChildId==childId);
        }

        public List<Decisions> GetChildListByUserId(long userId)
        {
            return GetDbSet().Where(d => d.UserId == userId).ToList();
        }
    }
}
