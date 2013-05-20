using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class PreexistingSupportFormRepository : FormRepository<PreexistingSupportForm>, IPreexistingSupportFormRepository
    {
        public PreexistingSupportFormRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}