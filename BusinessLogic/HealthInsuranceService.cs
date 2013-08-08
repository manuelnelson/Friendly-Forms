using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class HealthInsuranceService : FormService<IHealthInsuranceRepository, HealthInsurance>, IHealthInsuranceService
    {
        public HealthInsuranceService(IHealthInsuranceRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
