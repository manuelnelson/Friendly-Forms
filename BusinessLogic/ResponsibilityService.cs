using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ResponsibilityService : Service<ResponsibilityRepository,Responsibility,ResponsibilityViewModel>, IResponsibilityService
    {
        public ResponsibilityService(ResponsibilityRepository formRepository) : base(formRepository)
        {
        }
    }
}
