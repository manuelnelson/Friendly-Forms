using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class OtherChildrenRepository : FormRepository<OtherChildren>, IOtherChildrenRepository
    {
        public OtherChildrenRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
