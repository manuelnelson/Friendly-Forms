using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class HealthService : FormService<IHealthRepository, Health>, IHealthService
    {
        private IHealthRepository HealthRepository { get; set; }

        public HealthService(IHealthRepository repository) : base(repository)
        {
            HealthRepository = repository;
        }
    }
}
