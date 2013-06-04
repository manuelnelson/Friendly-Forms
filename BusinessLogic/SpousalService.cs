using BusinessLogic.Contracts;
using DataInterface;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class SpousalService : FormService<ISpousalRepository, SpousalSupport, SpousalViewModel>, ISpousalService
    {
        public SpousalService(ISpousalRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
