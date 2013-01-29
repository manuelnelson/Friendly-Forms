using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IHealthInsuranceService : IFormService<IHealthInsuranceRepository, HealthInsurance>
    {
    }
}
