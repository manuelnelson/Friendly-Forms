using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class ExtraDecisionRepository : FormRepository<ExtraDecisions>, IExtraDecisionRepository
    {
        public ExtraDecisionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
