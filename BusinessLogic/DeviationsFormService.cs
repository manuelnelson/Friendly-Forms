using BusinessLogic.Contracts;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class DeviationsFormService : FormService<IDeviationsFormRepository, DeviationsForm, DeviationsFormViewModel>, IDeviationsFormService
    {
        private IDeviationsFormRepository DeviationsFormRepository { get; set; }

        public DeviationsFormService(IDeviationsFormRepository repository) : base(repository)
        {
            DeviationsFormRepository = repository;
        }

    }
}
