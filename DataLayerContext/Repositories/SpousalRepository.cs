using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class SpousalRepository : FormRepository<SpousalSupport>, ISpousalRepository
    {
        public SpousalRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
