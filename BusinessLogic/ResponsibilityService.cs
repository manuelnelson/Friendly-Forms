using BusinessLogic.Contracts;
using DataInterface;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ResponsibilityService : FormService<IResponsibilityRepository,Responsibility,ResponsibilityViewModel>, IResponsibilityService
    {
        public ResponsibilityService(IResponsibilityRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
