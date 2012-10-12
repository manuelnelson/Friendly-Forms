using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class ExtraDecisionRepository : Repository<ExtraDecisions>, IExtraDecisionRepository
    {
        public ExtraDecisionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
