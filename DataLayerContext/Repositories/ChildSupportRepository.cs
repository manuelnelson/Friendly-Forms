using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class ChildSupportRepository : FormRepository<ChildSupport>, IChildSupportRepository
    {
        public ChildSupportRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
