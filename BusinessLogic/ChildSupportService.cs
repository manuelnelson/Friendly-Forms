using BusinessLogic.Contracts;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ChildSupportService : FormService<IChildSupportRepository, ChildSupport, ChildSupportViewModel>, IChildSupportService
    {
        public ChildSupportService(IChildSupportRepository formRepository) : base(formRepository)
        {
        }
    }
}
