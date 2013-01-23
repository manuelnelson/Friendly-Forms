using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IHealthInsuranceService : IService<IHealthInsuranceRepository, HealthInsurance>
    {
    }
}
