using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ChildFormService : FormService<ChildFormRepository, ChildForm, ChildFormViewModel>, IChildFormService
    {
        public ChildFormService(ChildFormRepository formRepository) : base(formRepository)
        {
        }
    }
}
