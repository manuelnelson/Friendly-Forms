using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class HealthInsuranceService : Service<HealthInsuranceRepository, HealthInsurance, HealthInsuranceViewModel>, IHealthInsuranceService
    {
        public HealthInsuranceService(HealthInsuranceRepository formRepository) : base(formRepository)
        {
        }
    }
}
