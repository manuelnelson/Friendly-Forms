using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class OtherChildRepository : FormRepository<OtherChild>,IOtherChildRepository
    {
        public OtherChildRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
