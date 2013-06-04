using BusinessLogic.Contracts;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class HealthInsuranceService : FormService<IHealthInsuranceRepository, HealthInsurance, HealthInsuranceViewModel>, IHealthInsuranceService
    {
        public HealthInsuranceService(IHealthInsuranceRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
