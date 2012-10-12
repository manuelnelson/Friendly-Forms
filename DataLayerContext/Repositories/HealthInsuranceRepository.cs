using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class HealthInsuranceRepository : FormRepository<HealthInsurance>, IHealthInsuranceRepository
    {
        public HealthInsuranceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
