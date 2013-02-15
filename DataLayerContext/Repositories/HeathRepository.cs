using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class HealthRepository : FormRepository<Health>, IHealthRepository
    {
        public HealthRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}