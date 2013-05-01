using BusinessLogic.Contracts;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class HealthService : FormService<IHealthRepository, Health, HealthViewModel>, IHealthService
    {
        private IHealthRepository HealthRepository { get; set; }

        public HealthService(IHealthRepository repository) : base(repository)
        {
            HealthRepository = repository;
        }
    }
}
