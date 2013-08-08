using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class ChildSupportService : FormService<IChildSupportRepository, ChildSupport>, IChildSupportService
    {
        public ChildSupportService(IChildSupportRepository formRepository) : base(formRepository)
        {
        }
    }
}
