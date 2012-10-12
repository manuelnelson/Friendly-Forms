using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class PreexistingSupportRepository : FormRepository<PreexistingSupport>, IPreexistingSupportRepository
    {
        public PreexistingSupportRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
