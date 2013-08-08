using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class ChildFormService : FormService<IChildFormRepository, ChildForm>, IChildFormService
    {
        public ChildFormService(IChildFormRepository formRepository) : base(formRepository)
        {
        }
    }
}
