using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class DeviationsRepository : FormRepository<Deviations>, IDeviationsRepository
    {
        public DeviationsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}