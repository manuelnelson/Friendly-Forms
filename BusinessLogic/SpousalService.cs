using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class SpousalService : FormService<SpousalRepository, SpousalSupport, SpousalViewModel>, ISpousalService
    {
        public SpousalService(SpousalRepository formRepository) : base(formRepository)
        {
        }
    }
}
