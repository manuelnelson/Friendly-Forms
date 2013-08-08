using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class ChildCareFormService : FormService<IChildCareFormRepository, ChildCareForm>, IChildCareFormService
    {
        private IChildCareFormRepository ChildCareFormRepository { get; set; }

        public ChildCareFormService(IChildCareFormRepository repository) : base(repository)
        {
            ChildCareFormRepository = repository;
        }
    }
}
