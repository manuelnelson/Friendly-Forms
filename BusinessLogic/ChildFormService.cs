using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ChildFormService : Service<ChildFormRepository, ChildForm, ChildFormViewModel>, IChildFormService
    {
        public ChildFormService(ChildFormRepository formRepository) : base(formRepository)
        {
        }
    }
}
