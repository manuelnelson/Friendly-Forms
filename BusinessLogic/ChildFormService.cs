using BusinessLogic.Contracts;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ChildFormService : FormService<IChildFormRepository, ChildForm, ChildFormViewModel>, IChildFormService
    {
        public ChildFormService(IChildFormRepository formRepository) : base(formRepository)
        {
        }
    }
}
