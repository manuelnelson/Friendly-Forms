using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ChildSupportService : Service<ChildSupportRepository, ChildSupport, ChildSupportViewModel>, IChildSupportService
    {
        public ChildSupportService(ChildSupportRepository formRepository) : base(formRepository)
        {
        }
    }
}
