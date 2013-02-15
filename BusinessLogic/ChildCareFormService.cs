using BusinessLogic.Contracts;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ChildCareFormService : FormService<IChildCareFormRepository, ChildCareForm, ChildCareFormViewModel>, IChildCareFormService
    {
        private IChildCareFormRepository ChildCareFormRepository { get; set; }

        public ChildCareFormService(IChildCareFormRepository repository) : base(repository)
        {
            ChildCareFormRepository = repository;
        }
    }
}
