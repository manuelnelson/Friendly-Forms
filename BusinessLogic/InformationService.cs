using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class InformationService : Service<InformationRepository, Information, InformationViewModel>, IInformationService
    {
        public InformationService(InformationRepository formRepository) : base(formRepository)
        {
        }
    }
}
