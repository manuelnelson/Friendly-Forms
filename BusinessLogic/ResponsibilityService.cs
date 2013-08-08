using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class ResponsibilityService : FormService<IResponsibilityRepository, Responsibility>, IResponsibilityService
    {
        public ResponsibilityService(IResponsibilityRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
