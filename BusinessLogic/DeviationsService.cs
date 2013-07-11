using BusinessLogic.Contracts;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class DeviationsService : FormService<IDeviationsRepository, Deviations, DeviationsViewModel>, IDeviationsService
    {
        private IDeviationsRepository DeviationsRepository { get; set; }

        public DeviationsService(IDeviationsRepository repository) : base(repository)
        {
            DeviationsRepository = repository;
        }

    }
}
