using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class DeviationsFormRepository : FormRepository<DeviationsForm>, IDeviationsFormRepository
    {
        public DeviationsFormRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}