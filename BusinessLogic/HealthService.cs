using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class HealthService : FormService<HealthInsuranceRepository, HealthInsurance, HealthViewModel>, IHealthService
    {
        public HealthService(HealthInsuranceRepository formRepository) : base(formRepository)
        {
        }
    }
}
